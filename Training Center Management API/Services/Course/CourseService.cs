using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Models;
using Training_Center_Management_API.Dtos.Courses;
using Training_Center_Management_API.Dtos.common;
using Microsoft.AspNetCore.Mvc;

namespace Training_Center_Management_API.Services.Course
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .AsNoTracking()
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,

                    DepartmentId = c.DepartmentId,
                    DurationInHours = c.DurationInHours,

                    DepartmentName = c.Department.Name
                })
                .ToListAsync();
        }




        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,

                    DepartmentId = c.DepartmentId,
                    DurationInHours = c.DurationInHours,

                    DepartmentName = c.Department.Name
                })
                .FirstOrDefaultAsync();
        }



        public async Task<CourseDto?> CreateAsync(CreateCourseDto dto)
        {
            // نتأكد إن Department موجود
            var departmentExists = await _context.Departments
                .Where(s => s.Id == dto.DepartmentId)
                .Select(s => s.Name)
                .FirstOrDefaultAsync();

            var courseexist = await _context.Courses              // تم تعديل
                .AnyAsync(c=> c.Name == dto.Name);

            //.AnyAsync(d => d.Id == dto.DepartmentId);

            if (departmentExists == null || courseexist)
            {
                return null;
            }



            var course = new Models.Course 
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DurationInHours = dto.DurationInHours,
                DepartmentId = dto.DepartmentId
            };

            _context.Courses.Add(course);

            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                DepartmentId = course.DepartmentId,

                // هنجيب الاسم لأن الـ Navigation
                // Property مش هتكون Loaded تلقائيًا
                DepartmentName = departmentExists
                //(await _context.Departments
                //    .Where(d => d.Id == course.DepartmentId)
                //    .Select(d => d.Name)
                //    .FirstAsync())
            };
        }




        public async Task<bool> UpdateAsync(int id, UpdateCourseDto dto)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return false;
            }

            var departmentExists = await _context.Departments
                .AnyAsync(d => d.Id == dto.DepartmentId);

            if (!departmentExists)
            {
                return false;
            }

            var foundCourseWithSameName = await _context.Courses
                .AnyAsync(c => c.Name == dto.Name );
            if (foundCourseWithSameName)
            {
                return false;
            }


            course.Name = dto.Name;
            course.Description = dto.Description;
            course.Price = dto.Price;
            course.DepartmentId = dto.DepartmentId;
            course.DurationInHours = dto.DurationInHours;


            await _context.SaveChangesAsync();

            return true;
        }





        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return false;
            }

            // Soft Delete
            course.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;

        }



        public async Task<CourseDetails> CourseDetailsAsync(int id)
        {

            var details = await _context.Courses
                .Where(c => c.Id == id)
                .AsNoTracking()
                .Select(c => new CourseDetails
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    DurationInHours = c.DurationInHours,
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.Department.Name,


                    //instructorNames = c.CourseInstructors
                    //.Select(i => i.Instructor.FullName)
                    //.ToList(),


                    Enrollments = c.Enrollments
                    .Select(e => new courseEnrollmentDto
                    {
                        StudentName = e.Student.FullName,
                        Grade = e.Grade,
                        Status = e.Status,
                        EnrollmentDate = e.EnrollmentDate,
                    })
                    .ToList()

                }
                )
                .FirstOrDefaultAsync();


            return details;


        }

        public async Task<PagedResultDto<CourseDto>> CourseSearchAsync([FromQuery] CourseSearch search)
        {
            var query = _context.Courses.AsQueryable();

            if(!string.IsNullOrEmpty(search.SearchName))
            {
                query = query.Where(q => q.Name.Contains(search.SearchName));
            }

            if (!string.IsNullOrEmpty(search.SortBy))
            {



                switch (search.SortBy?.ToLower())
                {
                    case "name":
                        query = (search.Descending) ? query.OrderByDescending(q => q.Name) : query.OrderBy(q => q.Name);
                        break;


                    case "price":
                        query = search.Descending ? query.OrderByDescending(q => q.Price) : query.OrderBy(q => q.Price);
                        break;


                    default:
                        query = query.OrderBy(q => q.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(q => q.Id);
            }


            var totalItems = await query.CountAsync();
            var pagenumber = search.PageNumber < 1 ? 1 : search.PageNumber;

            var courses = await query
                .Skip((pagenumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    DurationInHours = c.DurationInHours,
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.Department.Name
                })
                .ToListAsync();

            var pageTolal = (int)Math.Ceiling((double)totalItems / search.PageSize);

            return new PagedResultDto<CourseDto>
            {
                Page= pagenumber,
                PageSize= search.PageSize,
                TotalPages= pageTolal,
                TotalCount= totalItems,
                Data= courses

            };

        }
    }
}
