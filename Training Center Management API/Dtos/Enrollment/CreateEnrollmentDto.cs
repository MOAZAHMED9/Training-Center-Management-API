using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Enrollment
{
    public class CreateEnrollmentDto
    {
        //[Range(1, int.MaxValue)]
        //public int StudentId { get; set; }

        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Status { get; set; } = "Active";

    }
}
