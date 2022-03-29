namespace EmployeesAPI.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Region Region { get; set; }
    }
}
