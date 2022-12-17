using System.Text.Json.Serialization;

namespace EmployeeAPI.Contracts.Input
{
    public class Region
    {
        public int Id { get; }
        public string Name { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? RegionId { get; }

        [JsonConstructor]
        private Region(int id, string name, int? regionId)
        {
            Id = id;
            Name = name;
            RegionId = regionId;
        }

        public static Region Create(int id, string name, int? regionId) => new Region(id, name, regionId);
    }
}