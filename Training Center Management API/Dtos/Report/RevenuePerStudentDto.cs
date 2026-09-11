namespace Training_Center_Management_API.Dtos.Report
{
    public class RevenuePerStudentDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public decimal TotalRevenue { get; set; }
    }
}
