using System.Text.Json.Serialization;

namespace EmployeeAPI.Contracts.Input
{
    public class Region
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        private Region(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Region Create(int id, string name) => new Region(id, name);
    }
}
