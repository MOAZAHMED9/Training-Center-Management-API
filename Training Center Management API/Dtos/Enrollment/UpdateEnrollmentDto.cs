using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Enrollment
{
    public class UpdateEnrollmentDto
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;

        [Range(0, 100)]
        public decimal Grade { get; set; }

    }
}
