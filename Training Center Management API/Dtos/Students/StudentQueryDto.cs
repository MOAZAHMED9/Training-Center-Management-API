using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.DTOs.Students
{
    public class StudentQueryDto
    {
        public string? Search { get; set; }
        public string? Email { get; set; }

        public string? SortBy  { get; set; }

        public bool Descending { get; set; } = false;


        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 50)]
        public int PageSize { get; set; } = 10;
    }
}