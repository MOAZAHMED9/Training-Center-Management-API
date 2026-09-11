namespace Training_Center_Management_API.Models
{
    public class CourseInstructor : BaseEntity
    {
        public int CourseId { get; set; }

        public int InstructorId { get; set; }

        public Course Course { get; set; }

        public Instructor Instructor { get; set; }
    }
}
