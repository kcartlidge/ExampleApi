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
        /// <summary>
        /// Internal store of people. Does not survive restarts.
        /// </summary>
        private static List<Person> people = new();

        /// <summary>Gets a list of all the people.</summary>
        /// <remarks>Simulated async for demonstration purposes.</remarks>
        public static async Task<List<Person>> GetPeople()
        {
            EnsurePeople();
            return people;
        }

        /// <summary>Add a new person to the list of people.</summary>
        /// <remarks>Simulated async for demonstration purposes.</remarks>
        public static async Task AddPerson(Person person)
        {
            EnsurePeople();
            people.Add(person);
        }

        private static void EnsurePeople()
        {
            // If the list is empty, generate 10 people.
            // This is a hack to ensure sample data as we have no constructor.
            // It will be replaced in due course.
            if (people == null || people.Count == 0)
            {
                people = Enumerable.Range(1, 5).Select(index => new Person
                {
                    ID = index,
                    Name = $"Person #{index}",
                    Age = 18 + (2 * index),
                }).ToList();
            }
        }
    }
}
