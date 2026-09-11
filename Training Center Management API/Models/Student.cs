namespace Training_Center_Management_API.Models
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public  string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Certificate> Certificates { get; set; }



        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}
