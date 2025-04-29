using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{

    [Table("Students")]
    public class Students
    {
        [Key]
        public int StudentId { get; set; }

        public string? StudentName { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public List<Enrollment> Enrollment { get; set; }

    }
}
