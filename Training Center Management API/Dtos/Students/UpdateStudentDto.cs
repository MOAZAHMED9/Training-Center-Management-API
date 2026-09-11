using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Students
{
    public class UpdateStudentDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress (ErrorMessage =" Email is invalid") ]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }


        [Required]
        public string Address { get; set; }
    }
}
