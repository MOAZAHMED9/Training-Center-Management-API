using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.CourseInstructor
{
    public class CreateCourseInstructorDto
    {
        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }

        [Range(1, int.MaxValue)]
        public int InstructorId { get; set; }
    }
}
