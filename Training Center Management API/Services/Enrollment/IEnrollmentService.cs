using Training_Center_Management_API.Dtos.Enrollment;

namespace Training_Center_Management_API.Services.Enrollment
{
    public interface IEnrollmentService
    {
        Task<List<EnrollmentDto>> GetAllAsync();

        Task<EnrollmentDto?> GetByIdAsync(int id);

        Task<EnrollmentDto?> CreateAsync( CreateEnrollmentDto dto);

        Task<bool> UpdateAsync(int id, UpdateEnrollmentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
