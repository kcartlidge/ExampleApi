using ExampleApi.Middleware;
using ExampleApi.Swagger;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ExampleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(o =>
            {
                // Turn off the automatic ModelState interception and standard response model.
                o.SuppressModelStateInvalidFilter = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Set the API metadata.
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Example API",
                    Version = "v1",
                    Description = "An example DotNet 6+ API",
                    TermsOfService = new Uri("https://example.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "The Dev Team",
                        Url = new Uri("https://example.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "API License",
                        Url = new Uri("https://choosealicense.com/licenses")
                    },
                });

                // Require a correlation ID.
                c.OperationFilter<CorrelationIdAttribute>();

                // Requires XML summary during Build (default location).
                // Decorates the Swagger stuff with more details.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddTransient<DurationMiddleware>();
            var app = builder.Build();
            app.UseDurationMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                // Remove the "Try it Out" button.
                app.UseSwaggerUI(o =>
                {
                    o.EnableTryItOutByDefault();
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    o.RoutePrefix = "docs";
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
