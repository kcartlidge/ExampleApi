using ExampleApi.Middleware;
using Microsoft.OpenApi.Models;

namespace ExampleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Set the API title.
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1", });
            });

            builder.Services.AddTransient<DurationMiddleware>();
            var app = builder.Build();
            app.UseDurationMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                // Remove the "Try it Out" button.
                app.UseSwaggerUI(o => o.EnableTryItOutByDefault());
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
