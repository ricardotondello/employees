namespace Employee.Domain
{
    public class Employee
    {
        public Guid Id { get; }
        public string Name { get; }
        public Region Region { get;  }

        private Employee(Guid id, string name, Region region)
        {
            Id = id;
            Name = name;
            Region = region;
        }

        public static Employee Create(Guid id, string name, Region region) =>
            new Employee(Guid.NewGuid(), name, region);
    }
}
