using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]

        public int InstructorId { get; set; }

        public string? InstructorName { get; set; }
            public string? UserId { get; set; }
            [ForeignKey("UserId")]
            public ApplicationUser? User { get; set; }

            public List<Course> Courses { get; set; }

    }
}
