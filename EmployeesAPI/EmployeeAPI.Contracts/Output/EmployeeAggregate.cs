using System.Text.Json.Serialization;

namespace EmployeeAPI.Contracts.Output
{
    public class EmployeeAggregate
    {
        public string FullName { get; }
        public IEnumerable<Region> Regions { get; }

        [JsonConstructor]
        private EmployeeAggregate(string fullName, IEnumerable<Region> regions)
        {
            FullName = fullName;
            Regions = regions ?? Enumerable.Empty<Region>();
        }

        public static EmployeeAggregate Create(string fullName, IEnumerable<Region> regions) =>
            new EmployeeAggregate(fullName, regions);
    }
}
