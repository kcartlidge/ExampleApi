#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleApi.Models
{
    /// <summary>
    /// Represents any type of error generated by the API.
    /// Actual exceptions may differ.
    /// </summary>
    public class ApiError
    {
        /// <summary>Key for any related error glossary/dictionary.</summary>
        public string Code { get; private set; }

        /// <summary>Informative summary of the problem.</summary>
        public string Details { get; private set; }

        /// <summary>
        /// Any errors raised. This is keyed by (for example) a request model
        /// field name, and the value is one or more errors for that key.
        /// </summary>
        public Dictionary<string, List<string>> Errors { get; set; }
            = new Dictionary<string, List<string>>();

        /// <summary>Create a BadRequest response.</summary>
        public static ActionResult BadRequest(
            string code,
            string details)
        {
            var errors = new Dictionary<string, List<string>>();
            return new BadRequestObjectResult(new ApiError(code, details, errors));
        }

        /// <summary>Create a BadRequest response.</summary>
        public static ActionResult BadRequest(
            string code,
            string details,
            ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, List<string>>();

            // If there are any ModelState errors, map them across.
            if (modelState != null && modelState.Count > 0)
                foreach (var key in modelState.Keys)
                {
                    var item = modelState[key];
                    if (item != null && item.Errors.Count > 0)
                        errors[key] = item.Errors.Select(x => x.ErrorMessage).ToList();
                }

            return new BadRequestObjectResult(new ApiError(code, details, errors));
        }

        /// <summary>Create a NotFound response.</summary>
        public static ActionResult NotFound(string code, string details)
        {
            var errors = new Dictionary<string, List<string>>();
            return new NotFoundObjectResult(new ApiError(code, details, errors));
        }

        /// <summary>Create a populated API error model.</summary>
        /// <remarks>Private as should be using specific methods (eg BadRequest).</remarks>
        private ApiError(
            string code,
            string details,
            Dictionary<string, List<string>> errors
            )
        {
            Code = code;
            Details = details;
            Errors = errors;
        }
    }
}
