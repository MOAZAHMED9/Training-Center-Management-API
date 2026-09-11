using Training_Center_Management_API.Dtos.Department;

namespace Training_Center_Management_API.Services.Department
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto?> GetByIdAsync(int id);

        Task<DepartmentDto?> CreateAsync(CreateDepartmentDto dto);

        Task<bool> UpdateAsync(int id, UpdateDepartmentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
