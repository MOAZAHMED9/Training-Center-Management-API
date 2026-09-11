using Training_Center_Management_API.Dtos.Instructors;

namespace Training_Center_Management_API.Services.Instrucrot
{
    public interface IInstructorService
    {
        Task<List<InstructorDto>> GetAllAsync();

        Task<InstructorDto?> GetByIdAsync(int id);

        Task<InstructorDto?> CreateAsync(CreateInstructorDto dto);

        Task<bool> UpdateAsync(int id,UpdateInstructorDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
