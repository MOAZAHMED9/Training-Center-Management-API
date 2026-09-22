using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.common;
using Training_Center_Management_API.Dtos.Report;
using Training_Center_Management_API.Dtos.Students;
using Training_Center_Management_API.DTOs.Students;
using Training_Center_Management_API.Models;
namespace Training_Center_Management_API.Services.Student
{


    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;                        //

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {

            var students = await _context.Students
                .AsNoTracking()
                //.Where(s => !s.IsDeleted)
                .Select(s => new StudentDto              //projection
                {

                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    BirthDate = s.BirthDate,
                    Address = s.Address,
                    
                    EnrollmentDate = s.EnrollmentDate
                })
                .ToListAsync();

            return students;

        }


        public async Task<StudentDto?> GetByIdAsync(int id)
        {

            if (id <= 0 )
            {
                return null;
            }

           

            var student = await _context.Students
                .Where(s => s.Id == id && !s.IsDeleted)
                .Select(s => new StudentDto
                {
                    //Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    BirthDate = s.BirthDate,
                    Address = s.Address,
                    EnrollmentDate = s.EnrollmentDate
                })
                .FirstOrDefaultAsync();


            return student;

        }





        public async Task<StudentDto?> CreateAsync(CreateStudentDto dto)
        {
            var studentExist = await _context.Students
                .AnyAsync(s => s.Email == dto.Email);

            if(studentExist)
            {
                return null;
            }


            var student = new Models.Student
            {

                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                BirthDate = dto.BirthDate,
                Address = dto.Address,

                EnrollmentDate = DateTime.UtcNow,

                
                IsDeleted = false

            };

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return new StudentDto
            {
                //Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                BirthDate = student.BirthDate,
                EnrollmentDate = student.EnrollmentDate

            };


        }

        public async Task<bool> UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return false;
            }

            student.FullName = dto.FullName;
            student.Phone = dto.Phone;
            student.Address = dto.Address;
            student.Email = dto.Email;
            


            await _context.SaveChangesAsync();

            return true;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return false;
            }


           
            student.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }





        



        public async Task<StudentDetailsDto>? GetStudentDetails(int id)
        {

            var student = await _context.Students
                .Where(s => s.Id ==id)
                .Select(s => new StudentDetailsDto
                {
                    
                    //Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    EnrollmentDate = s.EnrollmentDate,

                    Enrollments = s.Enrollments
                        .Select(e => new studentEnrollmentDto
                        {
                            //CourseId = e.CourseId,
                            CourseName = e.Course.Name,
                            Grade = e.Grade,
                            Status = e.Status,
                            EnrollmentDate = e.EnrollmentDate
                        })
                        .ToList(),

                    Payments = s.Payments
                        .Select(p => new PaymentDto
                        {
                            Amount = p.Amount,
                            PaymentDate = p.PaymentDate,
                            PaymentMethod = p.PaymentMethod
                        })
                        .ToList(),

                    Certificates = s.Certificates
                        .Select(c => new CertificateDto
                        {
                            CertificateNumber = c.CertificateNumber,
                            CourseName = c.Course.Name,
                            IssueDate = c.IssueDate,
                            Grade = c.Grade
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();



            return student;


        }




        //public async Task<PagedResultDto<StudentSearchDto>> SearchStudents(
        //string? search,
        //string? email,
        //int page = 1,
        //int pageSize = 5,
        //string? sortBy = null,
        //bool descending = false)



        public async Task<PagedResultDto<StudentSearchDto>> SearchStudents(StudentQueryDto dto)
        {


            var query = _context.Students
                .AsQueryable();



            if (!string.IsNullOrWhiteSpace(dto.Search))
            {
                query = query.Where(s =>
                    s.FullName.Contains(dto.Search));
            }



            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                query = query.Where(s =>
                    s.Email.Contains(dto.Email));
            }







            if (!string.IsNullOrWhiteSpace(dto.SortBy))
            {
                switch (dto.SortBy.ToLower())
                {
                    case "name":

                        query = dto.Descending
                            ? query.OrderByDescending(s => s.FullName)
                            : query.OrderBy(s => s.FullName);

                        break;


                    case "email":

                        query = dto.Descending
                            ? query.OrderByDescending(s => s.Email)
                            : query.OrderBy(s => s.Email);

                        break;


                    case "date":

                        query = dto.Descending
                            ? query.OrderByDescending(s => s.EnrollmentDate)
                            : query.OrderBy(s => s.EnrollmentDate);

                        break;


                    default:

                        query = query.OrderBy(s => s.Id);

                        break;
                }
            }
            else
            {
                query = query.OrderBy(s => s.Id);
            }




            //Pagination


            var pageNumber = dto.PageNumber < 1 ? 1 : dto.PageNumber;

            var students = await query
                .Skip((pageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .Select(s => new StudentSearchDto
                {
                    //Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    EnrollmentDate = s.EnrollmentDate
                })
                .ToListAsync();



            var totalCount = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(
                (double)totalCount / dto.PageSize);


            

            var result = new PagedResultDto<StudentSearchDto>
            {
                Page = pageNumber,
                PageSize = dto.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = students
            };


            return result;


        }

        public async Task<bool> EditRoleToInstructor(int userid)
        {
            

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userid);

            if (user == null)
            {
                return false;
            }

            user.Role = "Instructor";

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userid);

            if (student == null)
            {
                return false;
            }

            var instructor = new Instructor
            {
                UserId = user.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                Specialization = "Not Specified",
                Salary = 0,

            };

            await _context.Instructors.AddAsync(instructor);
             student.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;

        }
    }
}
