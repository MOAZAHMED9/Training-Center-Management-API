namespace Training_Center_Management_API.Dtos.Instructors
{
    public class InstructorDto
    {
        public int Id { get; set; }  
        public string FullName { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public string? Specialization { get; set; }

        public decimal Salary { get; set; }

    }
}
