using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    [Table("Grades")]
    public class Grades
    {
        [Key]
        public int GradeId { get; set; }
        public char Grade { get; set; }
    }

}
