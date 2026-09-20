using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Services.DashBoard;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/Dashboart")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }




        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _dashboardService.GetSummaryAsync();

            return Ok(result);
        }






        [HttpGet("top-students")]
        public async Task<IActionResult> GetTopStudents()
        {
            var result = await _dashboardService.GetTopAsync();

            return Ok(result);
        }




        [HttpGet("students-per-course")]
        public async Task<IActionResult> GetStudentsPerCourse()
        {
            var result = await _dashboardService.GetStudentsPerCourseAsync();

            return Ok(result);
        }






        [HttpGet("revenue-per-student")]
        public async Task<IActionResult> GetRevenuePerStudent()
        {
            var result = await _dashboardService.GetRevenuePerStudentAsync();

            return Ok(result);
        }

    }
}