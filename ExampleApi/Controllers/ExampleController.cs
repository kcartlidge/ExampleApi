using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        // GET: api/<ExampleController>
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await Data.PeopleRepo.GetPeople();
        }
    }
}
