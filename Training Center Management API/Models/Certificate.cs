namespace Training_Center_Management_API.Models
{
    public class Certificate : BaseEntity
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public string CertificateNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public string Grade { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}
