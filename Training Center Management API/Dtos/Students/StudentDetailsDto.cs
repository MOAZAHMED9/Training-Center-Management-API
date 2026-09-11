namespace Training_Center_Management_API.Dtos.Students
{
    public class StudentDetailsDto
    {
        //public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public List<studentEnrollmentDto>? Enrollments { get; set; }

        public List<PaymentDto>? Payments { get; set; }

        public List<CertificateDto>? Certificates { get; set; }
    }


    public class studentEnrollmentDto
    {

        public string CourseName { get; set; }

        public decimal Grade { get; set; }

        public string Status { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }


    public class PaymentDto
    {
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }
    }


    public class CertificateDto
    {
        public string CertificateNumber { get; set; }

        public string CourseName { get; set; }

        public DateTime IssueDate { get; set; }

        public string Grade { get; set; }
    }
}
