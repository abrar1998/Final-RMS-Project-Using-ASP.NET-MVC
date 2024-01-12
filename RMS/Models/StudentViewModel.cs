using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class StudentViewModel
    {

        [Display(Name = "Name")]
        [Required]
        public string StudentName { get; set; }

        [Display(Name = "Father Name")]
        [Required]
        public string Parentage { get; set; }

        [Display(Name = "Age")]

        [Required]
        public int StudentAge { get; set; }

        [Display(Name = "Email")]

        [Required]
        [EmailAddress]
        public string StudentEmail { get; set; }

        [Display(Name = "Phone")]

        [Required]
        public string StudentPhone { get; set; }

        [Required]
        public string Adhaar { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public int Course { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Upload Photo")]
        [Required]
        public IFormFile StudentPhoto { get; set; }
    }
}
