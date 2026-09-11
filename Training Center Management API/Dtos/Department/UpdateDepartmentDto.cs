using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Department
{
    public class UpdateDepartmentDto
    {

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

    }
}
