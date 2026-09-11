namespace Training_Center_Management_API.Dtos.CourseInstructor
{
    public class CourseInstructorDto
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        public int InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
    }
}
