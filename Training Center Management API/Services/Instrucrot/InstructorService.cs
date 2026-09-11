using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Instructors;
using Training_Center_Management_API.Models;

namespace Training_Center_Management_API.Services.Instrucrot
{
    public class InstructorService : IInstructorService
    {

        private readonly AppDbContext _context;

        public InstructorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InstructorDto>> GetAllAsync()
        {
            return await _context.Instructors
                .AsNoTracking()
                .Select(i => new InstructorDto
                {
                    Id = i.Id,
                    FullName = i.FullName,
                    Email = i.Email,
                    Phone = i.Phone,
                    Specialization = i.Specialization,
                    Salary = i.Salary
                })
                .ToListAsync();
        }




        public async Task<InstructorDto?> GetByIdAsync(int id)
        {
            return await _context.Instructors
                .AsNoTracking()
                .Where(i => i.Id == id)
                .Select(i => new InstructorDto
                {
                    Id = i.Id,
                    FullName = i.FullName,
                    Email = i.Email,
                    Phone = i.Phone,
                    Specialization = i.Specialization,
                    Salary = i.Salary
                })
                .FirstOrDefaultAsync();
        }




        public async Task<InstructorDto?> CreateAsync( CreateInstructorDto dto)
        {
            var emailExists = await _context.Instructors
                .AnyAsync(i => i.Email == dto.Email);

            if (emailExists)
            {
                return null;
            }

            var instructor = new Instructor
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Specialization = dto.Specialization,
                Salary = dto.Salary
            };

            _context.Instructors.Add(instructor);

            await _context.SaveChangesAsync();

            return new InstructorDto
            {
                Id = instructor.Id,
                FullName = instructor.FullName,
                Email = instructor.Email,
                Phone = instructor.Phone,
                Specialization = instructor.Specialization,
                Salary = instructor.Salary
            };
        }





        public async Task<bool> UpdateAsync( int id, UpdateInstructorDto dto)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
            {
                return false;
            }

            instructor.FullName = dto.FullName;
            instructor.Phone = dto.Phone;
            instructor.Specialization = dto.Specialization;
            instructor.Salary = dto.Salary;

            await _context.SaveChangesAsync();

            return true;
        }





        public async Task<bool> DeleteAsync(int id)
        {
            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
            {
                return false;
            }

            instructor.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
