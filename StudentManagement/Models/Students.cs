using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{

    [Table("Students")]
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string? UserId { get; set; } 
        public ApplicationUser? User { get; set; }

    }
}
