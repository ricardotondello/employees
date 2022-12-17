namespace Employee.Domain
{
    public class Employee
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public Region Region { get; }

        private Employee(Guid id, string name, string surname, Region region)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Region = region;
        }

        public static Employee Create(Guid id, string name, string surname, Region region) =>
            new(Guid.NewGuid(), name, surname, region);
    }
}