using Training_Center_Management_API.Dtos.CourseInstructor;

namespace Training_Center_Management_API.Services.CourseInstructorService
{
    public interface ICourseInstructorService
    {
        Task<List<CourseInstructorDto>> GetAllAsync();

        Task<CourseInstructorDto?> CreateAsync( CreateCourseInstructorDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
