#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net;

namespace ExampleApi.Controllers
{
    /// <summary>Requests to show different example responses.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.NotFound)]
    public class ExampleController : ControllerBase
    {
        /// <summary>Example request returning OK with result data.</summary>
        [HttpGet]
        [Route("/OK")]
        public async Task<ActionResult<IEnumerable<Person>>> GetOK()
        {
            return await Data.PeopleRepo.GetPeople();
        }

        /// <summary>Example request returning 400 Bad Result with an ApiError response model.</summary>
        [HttpGet]
        [Route("/BadRequest")]
        public async Task<ActionResult<IEnumerable<Person>>> GetBadRequest()
        {
            return ApiError.BadRequest(ApiCode.MissingDetails, "An address must be provided.");
        }

        /// <summary>Example request returning 404 Not Found with an ApiError response model.</summary>
        [HttpGet]
        [Route("/NotFound")]
        public async Task<ActionResult<IEnumerable<Person>>> GetNotFound()
        {
            return ApiError.NotFound(ApiCode.NobodyFound, "No matching Person was found.");
        }
    }
}
