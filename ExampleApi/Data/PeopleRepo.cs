#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using ExampleApi.Models;

namespace ExampleApi.Data
{
    /// <summary>
    /// Sample data source of people.
    /// In a normal API this would be separate from this project, sat
    /// behind an interface, and obtained via dependency injection.
    /// That will come in a later commit. For the moment the commits
    /// are about how the data is accessed in the API, not where it
    /// comes from.
    /// </summary>
    public static class PeopleRepo
    {
        /// <summary>Gets a list of all the people.</summary>
        /// <remarks>Simulated async for demonstration purposes.</remarks>
        public static async Task<List<Person>> GetPeople()
        {
            // Generate 10 people.
            return Enumerable.Range(1, 5).Select(index => new Person
            {
                ID = index,
                Name = $"Person #{index}",
                Age = 18 + (2 * index),
            }).ToList();
        }
    }
}
