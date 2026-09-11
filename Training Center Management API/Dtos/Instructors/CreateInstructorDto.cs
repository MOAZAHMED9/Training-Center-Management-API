using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Instructors
{
    public class CreateInstructorDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Specialization { get; set; }

        [Range(0, 1000000)]
        public decimal Salary { get; set; }


    }
}
