using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class FeeDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TotalFee { get; set; }

        public int StudentId {  get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }


        [DataType(DataType.Date)]
        public DateOnly RegistrationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.Date);
    }
}
