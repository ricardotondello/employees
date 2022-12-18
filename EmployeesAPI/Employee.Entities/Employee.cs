using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Entities;

[Table("Employee")]
public sealed class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public Region Region { get; set; }
    public int? RegionId { get; set; }

    public Employee(string name, string surname, int? regionId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        RegionId = regionId;
    }
}