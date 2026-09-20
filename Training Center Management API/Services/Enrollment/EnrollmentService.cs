using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Enrollment;

namespace Training_Center_Management_API.Services.Enrollment
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<EnrollmentDto>> GetAllAsync()
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,

                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,

                    CourseId = e.CourseId,
                    CourseName = e.Course.Name,

                    EnrollmentDate = e.EnrollmentDate,

                    Status = e.Status,

                    Grade = e.Grade
                })
                .ToListAsync();
        }


        public async Task<EnrollmentDto?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,

                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,

                    CourseId = e.CourseId,
                    CourseName = e.Course.Name,

                    EnrollmentDate = e.EnrollmentDate,

                    Status = e.Status,

                    Grade = e.Grade
                })
                .FirstOrDefaultAsync();
        }


        public async Task<EnrollmentDto?> CreateAsync(CreateEnrollmentDto dto)
        {
           
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == dto.StudentId);

            if (!studentExists)
            {
                return null;
            }


            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == dto.CourseId);

            if (!courseExists)
            {
                return null;
            }


            var alreadyEnrolled =
                await _context.Enrollments
                    .AnyAsync(e => e.StudentId == dto.StudentId && e.CourseId == dto.CourseId);


            if (alreadyEnrolled)
            {
                return null;
            }


            var enrollment = new Models.Enrollment 
            {
                StudentId = dto.StudentId,

                CourseId = dto.CourseId,

                EnrollmentDate = DateTime.UtcNow,

                Status = dto.Status
            };


            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();


            
            return await GetByIdAsync(enrollment.Id);    //// بتحفظو يعدين تجيب الid من الداتا بيز
        }


        public async Task<bool> UpdateAsync( int id, UpdateEnrollmentDto dto)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
            {
                return false;
            }


            enrollment.Status = dto.Status;

            enrollment.Grade = dto.Grade;


            await _context.SaveChangesAsync();

            return true;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
            {
                return false;
            }


            // Soft Delete
            enrollment.IsDeleted = true;       /// لو حزفت ال child الparent مش هيتاثر  
            //اقدر اعملها hard كدا كدا دي علاقه 

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
