using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class StudentViewEdit
    {
        [Key]
        public int StudentId { get; set; }


        [Display(Name = "Name")]
        [Required]
        public string StudentName { get; set; }

        [Display(Name = "Father Name")]
        [Required]
        public string Parentage { get; set; }

        [Required]
        public int StudentAge { get; set; }

        [Required]
        [EmailAddress]
        public string StudentEmail { get; set; }

        [Required]
        public string StudentPhone { get; set; }

        [Required]
        public string Adhaar { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DOB { get; set; }

        [DataType(DataType.Date)]
        public DateOnly RegistrationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        public string? StudentPhoto { get; set; }


        public int Course { get; set; }
    }
}
