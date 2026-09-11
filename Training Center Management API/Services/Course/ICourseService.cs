using Training_Center_Management_API.Dtos.Courses;

namespace Training_Center_Management_API.Services.Course
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();

        Task<CourseDto?> GetByIdAsync(int id);

        Task<CourseDto?> CreateAsync(CreateCourseDto dto);

        Task<bool> UpdateAsync(int id, UpdateCourseDto dto);

        Task<bool> DeleteAsync(int id);

        Task<CourseDetails> CourseDetailsAsync(int id);
    }
}
