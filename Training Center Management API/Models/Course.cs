namespace Training_Center_Management_API.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int DurationInHours { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<CourseInstructor> CourseInstructors { get; set; }
    }
}
