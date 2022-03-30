using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesAPI.Entities
{
    [Table("Region")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Region Parent { get; set; }

        public Region(int id, string name, int? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }
    }
}