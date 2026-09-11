using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Courses
{
    public class CreateCourseDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 1000000)]
        public decimal Price { get; set; }


        [Range(1, 200)]
        public int DurationInHours { get; set; } = 0;

        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
    }
}
