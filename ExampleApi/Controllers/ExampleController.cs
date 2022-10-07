using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    /// <summary>Requests to show different example responses.</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        /// <summary>Example request returning OK with result data.</summary>
        [HttpGet]
        [Route("/OK")]
        public async Task<IEnumerable<Person>> GetOK()
        {
            return await Data.PeopleRepo.GetPeople();
        }

        /// <summary>Example request returning 400 Bad Result with an ApiError response model.</summary>
        [HttpGet]
        [Route("/BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("An address must be provided.");
        }

        /// <summary>Example request returning 404 Not Found with an ApiError response model.</summary>
        [HttpGet]
        [Route("/NotFound")]
        public IActionResult GetNotFound()
        {
            return NotFound("No matching Person was found.");
        }
    }
}
