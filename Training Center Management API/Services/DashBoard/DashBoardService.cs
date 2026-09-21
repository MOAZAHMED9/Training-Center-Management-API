using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.DashBoard;
using Training_Center_Management_API.Dtos.Report;

namespace Training_Center_Management_API.Services.DashBoard
{
    public class DashBoardService : IDashboardService
    {

        private readonly AppDbContext _context;

        public DashBoardService (AppDbContext context)
        {
            _context = context;
        }
        
            
        public async Task<DashboardDto> GetSummaryAsync()
        {
            return new DashboardDto()
            {
                TotalCourses =  await _context.Courses.CountAsync(),

                TotalEnrollments = await _context.Enrollments.CountAsync(),

                TotalPayments = await _context.Payments.SumAsync(s=> s.Amount),

                TotalInstructor= await _context.Instructors.CountAsync(),

                TotalStudents = await _context.Students.CountAsync()

            };



            //DashboardDto dto = new DashboardDto();

            //dto.TotalCourses = await _context.Courses.CountAsync();

            //dto.TotalEnrollments = await _context.Enrollments.CountAsync();

            //dto.TotalPayments = await _context.Payments.SumAsync(s=> s.Amount);

            //dto.TotalInstructor= await _context.Instructors.CountAsync();

            //dto.TotalStudents = await _context.Students.CountAsync();

            //return dto;



        }



        public async Task<List<TopStudent>> GetTopAsync()
        {
            return await _context.Enrollments
               .AsNoTracking()
               .GroupBy(s => new
               {
                   s.StudentId,
                   s.Student.FullName

               })
               .Select(g => new TopStudent
               {
                   fullname = g.Key.FullName,
                   AvgGrade = g.Average(s => s.Grade)

               })
               .OrderByDescending(s => s.AvgGrade)
               .Take(3)
               .ToListAsync();


            //var top = new List<TopStudent>();
            //foreach (var item in student)
            //{
            //    top.Add(item);

            //}

            //return top;


        }





        public async Task<List<StudentsPerCourseDto>> GetStudentsPerCourseAsync()
        {
            return await _context.Enrollments
                .AsNoTracking()

                .GroupBy(e => new
                {
                    e.CourseId,
                    e.Course.Name
                })

                .Select(g => new StudentsPerCourseDto
                {
                    CourseId = g.Key.CourseId,

                    CourseName = g.Key.Name,

                    StudentsCount = g.Count()
                })

                .OrderByDescending(x => x.StudentsCount)

                .ToListAsync();
        }




        public async Task<List<RevenuePerStudentDto>> GetRevenuePerStudentAsync()
        {
            return await _context.Payments
                .AsNoTracking()

                .GroupBy(p => new
                {
                    p.StudentId,
                    p.Student.FullName
                })

                .Select(g => new RevenuePerStudentDto
                {
                    StudentId = g.Key.StudentId,

                    StudentName = g.Key.FullName,

                    TotalRevenue = g.Sum(p => p.Amount)
                })

                .OrderByDescending(x => x.TotalRevenue)

                .ToListAsync();
        }

    }
}
