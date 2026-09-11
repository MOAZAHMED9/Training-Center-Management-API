namespace Training_Center_Management_API.Dtos.Report
{
    public class StudentsPerCourseDto
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = string.Empty;

        public int StudentsCount { get; set; }
    }
}
