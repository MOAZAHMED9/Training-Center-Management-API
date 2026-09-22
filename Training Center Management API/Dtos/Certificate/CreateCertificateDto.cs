namespace Training_Center_Management_API.Dtos.Certificate
{
    public class CreateCertificateDto
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public string CertificateNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public string Grade { get; set; }
    }
}
