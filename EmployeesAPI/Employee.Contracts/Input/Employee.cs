using System.Text.Json.Serialization;

namespace Employee.Contracts.Input;

public class Employee
{
    public string Name { get; }
    public string Surname { get; }
    public int RegionId { get; }

    [JsonConstructor]
    private Employee(string name, string surname, int regionId)
    {
        Name = name;
        RegionId = regionId;
        Surname = surname;
    }

    public static Employee Create(string name, string surname, int regionId) =>
        new Employee(name, surname, regionId);
}