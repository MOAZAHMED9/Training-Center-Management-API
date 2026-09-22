using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Auth
{
    public class RegisterStudentDto
    {
        [MinLength(5)]
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [Required]    
        public string Password { get; set; }

        [Required]    
        public string ConfirmPassword { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }
    }
}
