using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Courses
{
    public class CourseSearch
    {
        public string? SearchName { get; set; }

        public string? SortBy { get; set; }


        [Range(1, 20)]
        public int PageNumber { get; set; }

        [Range(1, 20)]
        public int PageSize { get; set; } 

        public bool Descending { get; set; }

    }
}
