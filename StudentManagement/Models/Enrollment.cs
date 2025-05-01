using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace StudentManagement.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        public DateOnly EnrollmentDate { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Students Students { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        //public int GradeId {  get; set; }
        //[ForeignKey("GradeId")]
        //public Grades Grade { get; set; } // 1-to-1 with Grade
   
    }

}
