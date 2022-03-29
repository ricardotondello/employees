using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesAPI.Entities
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        //public Region Parent { get; set; }

        public Region(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
    }
}