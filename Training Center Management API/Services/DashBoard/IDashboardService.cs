using Training_Center_Management_API.Dtos.DashBoard;
using Training_Center_Management_API.Dtos.Report;

namespace Training_Center_Management_API.Services.DashBoard
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetSummaryAsync();

        Task<List<TopStudent>> GetTopAsync();

        Task<List<StudentsPerCourseDto>> GetStudentsPerCourseAsync();

        Task<List<RevenuePerStudentDto>> GetRevenuePerStudentAsync();
    }
}
