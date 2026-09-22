using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Dtos.common;
using Training_Center_Management_API.Dtos.Report;
using Training_Center_Management_API.Dtos.Students;
using Training_Center_Management_API.DTOs.Students;

namespace Training_Center_Management_API.Services.Student
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllAsync();

        Task<StudentDto?> GetByIdAsync(int id);

        Task<StudentDto> CreateAsync(CreateStudentDto dto);

        Task<bool> UpdateAsync(int id, UpdateStudentDto dto);

        Task<bool> DeleteAsync(int id);

       

        Task <StudentDetailsDto> GetStudentDetails(int id);

        Task<PagedResultDto<StudentSearchDto>> SearchStudents(StudentQueryDto dto);

        //Task <PagedResultDto<StudentSearchDto>> SearchStudents(
        //string? search,
        //string? email,
        //int page = 1,
        //int pageSize = 5,
        //string? sortBy = null,
        //bool descending = false);
    
        
        Task <bool> EditRoleToInstructor(int studentId);

    }
}
