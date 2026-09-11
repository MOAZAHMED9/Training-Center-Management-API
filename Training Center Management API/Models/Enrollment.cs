namespace Training_Center_Management_API.Models
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public decimal Grade { get; set; }

        public string Status { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}
