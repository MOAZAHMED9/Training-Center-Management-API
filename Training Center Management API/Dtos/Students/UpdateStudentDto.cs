using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Students
{
    public class UpdateStudentDto
    {
        [Required(ErrorMessage = " This is Required")]
        [StringLength(200, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200)]
        public string Address { get; set; }
    }
}
