namespace Training_Center_Management_API.Dtos.Courses
{
    public class CourseDetails
    {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int DurationInHours { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public List<courseEnrollmentDto>? Enrollments { get; set; }

        //public List<string> instructorNames { get; set; } 
    }



    public class courseEnrollmentDto
    {
        public string StudentName { get; set; }

        public decimal Grade { get; set; }

        public string Status { get; set; }

        public DateTime EnrollmentDate { get; set; }


    }
    



}
