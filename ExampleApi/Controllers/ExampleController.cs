#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net;
using ExampleApi.Data;
using ExampleApi.Models.RequestModels;

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
        /// <remarks>
        /// This is a stub endpoint that is designed to allow the presentation
        /// and handling of successful requests to be tested.
        /// </remarks>
        [HttpGet]
        [Route("/people")]
        public async Task<ActionResult<IEnumerable<Person>>> GetOK()
        {
            return await Data.PeopleRepo.GetPeople();
        }

        /// <summary>Example POST request returning OK with result data.</summary>
        /// <remarks>
        /// This is a stub endpoint that is designed to allow the presentation
        /// and handling of successful POST requests to be tested.
        /// There is no backing store so changes are not persisted through restarts.
        /// 
        /// Sample request:
        /// 
        ///     POST /create
        ///     {
        ///         "name": "Person #6",
        ///         "age": 30
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("/people")]
        public async Task<ActionResult> PostOK(PersonRequestModel person)
        {
            // Add the new Person if they pass validation.
            // In reality, the ModelState checks are handled by Core itself
            // before this point and invalid requests don't even hit this
            // endpoint. This is a precautionary check.
            if (ModelState.IsValid)
            {
                // Safe to use the values as they passed the ModelState check.
                // Any business-related rules should be added down the chain.
                await Data.PeopleRepo.AddPerson(new Person
                {
                    Name = person.Name.Trim(),
                    Age = person.Age.GetValueOrDefault(),
                });
                return Ok();
            }

            // There are validation rules on the incoming model, however this
            // will never be reached as Core handles the ModelState errors
            // automatically before here. This response is primarily to satisfy
            // the method signature.
            return ApiError.BadRequest(ApiCode.ValidationFailed, "Validation has failed.");
        }

        /// <summary>Example request returning 400 Bad Result with an ApiError response model.</summary>
        /// <remarks>
        /// This is a stub endpoint that is designed to allow the presentation
        /// and handling of bad requests to be tested.
        /// </remarks>
        [HttpGet]
        [Route("/bad-request")]
        public async Task<ActionResult<IEnumerable<Person>>> GetBadRequest()
        {
            return ApiError.BadRequest(ApiCode.MissingDetails, "An address must be provided.");
        }

        /// <summary>Example request returning 404 Not Found with an ApiError response model.</summary>
        /// <remarks>
        /// This is a stub endpoint that is designed to allow the presentation
        /// and handling of not-found requests to be tested.
        /// </remarks>
        [HttpGet]
        [Route("/not-found")]
        public async Task<ActionResult<IEnumerable<Person>>> GetNotFound()
        {
            return ApiError.NotFound(ApiCode.NobodyFound, "No matching Person was found.");
        }
    }
}
