using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.CourseInstructor;
using Training_Center_Management_API.Models;
using Training_Center_Management_API.Services.CourseInstructorService;

namespace Training_Center_Management_API.Services.CourseInstructorService
{
    public class CourseInstructorService : ICourseInstructorService
    {
        private readonly AppDbContext _context;

        public CourseInstructorService(AppDbContext context)
        {
            _context = context;
        }





        public async Task<List<CourseInstructorDto>> GetAllAsync()
        {
            return await _context.CourseInstructors
                .AsNoTracking()
                .Select(ci => new CourseInstructorDto
                {
                    Id = ci.Id,

                    CourseId = ci.CourseId,
                    CourseName = ci.Course.Name,

                    InstructorId = ci.InstructorId,
                    InstructorName = ci.Instructor.FullName
                })
                .ToListAsync();
        }






        public async Task<CourseInstructorDto?> CreateAsync( CreateCourseInstructorDto dto)
        {
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == dto.CourseId);

            if (!courseExists)
            {
                return null;
            }

            var instructorExists = await _context.Instructors
                .AnyAsync(i => i.Id == dto.InstructorId);

            if (!instructorExists)
            {
                return null;
            }

            
            var alreadyAssigned =
                await _context.CourseInstructors
                    .AnyAsync(ci =>
                        ci.CourseId == dto.CourseId &&
                        ci.InstructorId == dto.InstructorId);

            if (alreadyAssigned)
            {
                return null;
            }

            var courseInstructor = new CourseInstructor
            {
                CourseId = dto.CourseId,
                InstructorId = dto.InstructorId
            };

            _context.CourseInstructors.Add(courseInstructor);

            await _context.SaveChangesAsync();

            return await _context.CourseInstructors
                .AsNoTracking()
                .Where(ci => ci.Id == courseInstructor.Id)
                .Select(ci => new CourseInstructorDto
                {
                    Id = ci.Id,

                    CourseId = ci.CourseId,
                    CourseName = ci.Course.Name,

                    InstructorId = ci.InstructorId,
                    InstructorName =
                        ci.Instructor.FullName
                })
                .FirstOrDefaultAsync();
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var courseInstructor =await _context.CourseInstructors
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (courseInstructor == null)
            {
                return false;
            }

            // هنا ممكن Hard Delete
            // لأنه مجرد Relationship Table

            _context.CourseInstructors .Remove(courseInstructor);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
