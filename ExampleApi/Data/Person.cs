namespace ExampleApi.Data
{
    /// <summary>A person in the system.</summary>
    public class Person
    {
        /// <summary>The person key.</summary>
        public int ID { get; set; }

        /// <summary>The full name.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Age (in whole years, rounded down).</summary>
        public int Age { get; set; }
    }
}
