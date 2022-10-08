#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations;

namespace ExampleApi.Models.RequestModels
{
    /// <summary>A person request model.</summary>
    public class PersonRequestModel
    {
        // Note the missing ID compared to the data model.
        // This is a RequestModel so the API consumer would not be
        // expected to provide that for calls passing in a Person
        // record (neither PUT nor POST, which we'll get to).

        /// <summary>The full name.</summary>
        [Required(ErrorMessage = "You must provide the name.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 150 characters.")]
        public string Name { get; set; }

        /// <summary>Age (in whole years, rounded down).</summary>
        [Required(ErrorMessage = "You must provide the age.")]
        [Range(16, 200, ErrorMessage = "We only support people aged 16 or over.")]
        public int? Age { get; set; }
    }
}
