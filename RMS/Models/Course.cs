using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public string CourseFee { get; set; }

        [Required]
        public string CoursePdf { get; set; }

        [DataType(DataType.Date)]
        public DateOnly RegistrationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        public List<Student> Students { get; set; }
    }
}
