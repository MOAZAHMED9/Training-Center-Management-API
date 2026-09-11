using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Department;

namespace Training_Center_Management_API.Services.Department
{
  
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToListAsync();
        }




        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .FirstOrDefaultAsync();
        }




        public async Task<DepartmentDto?> CreateAsync( CreateDepartmentDto dto)
        {
            var nameExists = await _context.Departments
                .AnyAsync(d => d.Name == dto.Name);

            if (nameExists)
            {
                return null;
            }

            var department = new Models.Department 
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Departments.Add(department);

            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
        }




        public async Task<bool> UpdateAsync(int id,  UpdateDepartmentDto dto)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return false;
            }

            var nameExists = await _context.Departments
                .AnyAsync(d =>
                    d.Name == dto.Name &&
                    d.Id != id);

            if (nameExists)
            {
                return false;
            }

            department.Name = dto.Name;
            department.Description = dto.Description;

            await _context.SaveChangesAsync();

            return true;
        }





        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return false;
            }

            var hasCourses = await _context.Courses
                .AnyAsync(c => c.DepartmentId == id);

            if (hasCourses)
            {
                return false;
            }

            department.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }





    }
}


