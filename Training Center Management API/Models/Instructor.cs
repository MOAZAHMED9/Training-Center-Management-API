using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Models
{
    public class Instructor : BaseEntity
    {
        public string FullName { get; set; }

        [EmailAddress (ErrorMessage ="Email is Invalid")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Specialization { get; set; }

        public decimal Salary { get; set; }

        public ICollection<CourseInstructor> CourseInstructors { get; set; }


        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}
