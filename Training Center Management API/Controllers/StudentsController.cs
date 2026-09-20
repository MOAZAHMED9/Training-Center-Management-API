using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.common;
using Training_Center_Management_API.Dtos.Students;
using Training_Center_Management_API.DTOs.Students;
using Training_Center_Management_API.Models;
using Training_Center_Management_API.Services.Student;


namespace Training_Center_Management_API.Controllers
{
    [Route("api/Student")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase                            
    {
       
        private readonly IStudentService _studentService;                        //

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }                                                              //




        [Authorize(Roles ="Admin,Instructor") ]
        [HttpGet ]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
          

            var students = await _studentService.GetAllAsync();

            if (students == null)
                return NotFound();

            return Ok(students);
        }


        [Authorize (Policy = "StudentOwner")]          
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentByID(int id)   
        {


            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);


        }





        [Authorize (Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto dto)
        {
            

            var student= await _studentService.CreateAsync(dto);

            if (student == null)
            {
                return BadRequest("Student Email Is Found");
            }

                return Ok(student);
            
     
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id , UpdateStudentDto dto)
        {
            

            if (!await _studentService.UpdateAsync(id, dto))
            {
                return NotFound();
            }

            return NoContent();
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
           

            if(!await _studentService.DeleteAsync(id))
            {
                return NotFound();
            }

            return NoContent();
        }






        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/details")]
        public async Task<ActionResult<StudentDetailsDto>> GetStudentDetails(int id)
        {
            

            var student = await _studentService.GetStudentDetails(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }



        //[HttpGet("{id}/details")]
        //public async Task<ActionResult<StudentDetailsDto>> GetStudentDetails(int id)
        //{
        //    var student = await _context.Students
        //        .Where(s => s.Id == id && !s.IsDeleted)
        //        .Include(s => s.Enrollments)
        //            .ThenInclude(e => e.Course)
        //        .Include(s => s.Payments)
        //        .Include(s => s.Certificates)
        //            .ThenInclude(c => c.Course)
        //        .FirstOrDefaultAsync();

        //    if (student == null)
        //        return NotFound();

        //    var result = new StudentDetailsDto
        //    {
        //        //Id = student.Id,
        //        FullName = student.FullName,
        //        Email = student.Email,
        //        EnrollmentDate = student.EnrollmentDate,

        //        Enrollments = student.Enrollments
        //            .Select(e => new EnrollmentDto
        //            {
        //                //CourseId = e.CourseId,
        //                CourseName = e.Course.Name,
        //                Grade = e.Grade,
        //                Status = e.Status,
        //                EnrollmentDate = e.EnrollmentDate
        //            })
        //            .ToList(),

        //        Payments = student.Payments
        //            .Select(p => new PaymentDto
        //            {
        //                Amount = p.Amount,
        //                PaymentDate = p.PaymentDate,
        //                PaymentMethod = p.PaymentMethod
        //            })
        //            .ToList(),

        //        Certificates = student.Certificates
        //            .Select(c => new CertificateDto
        //            {
        //                CertificateNumber = c.CertificateNumber,
        //                CourseName = c.Course.Name,
        //                IssueDate = c.IssueDate,
        //                Grade = c.Grade
        //            })
        //            .ToList()
        //    };

        //    return Ok(result);
        //}



        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////                     
        ///

        [Authorize (Roles ="Admin,Instractor" ) ]
        [HttpGet ("SearchStudent") ]
        public async Task<ActionResult<PagedResultDto<StudentSearchDto>>> SearchStudents([FromQuery]StudentQueryDto dto)    //?
        {
            var result = await _studentService.SearchStudents(dto);


            return Ok(result);

        }


        //[Authorize(Roles = "Admin,Instructor")]
        //[HttpGet("search")]
        //public async Task<ActionResult<PagedResultDto<StudentSearchDto>>> SearchStudents(
        //string? search,
        //string? email,
        //int page = 1,
        //int pageSize = 5,
        //string? sortBy = null,
        //bool descending = false)
        //{

        //    #region ifhjjghjhv
        //    //var query = _context.Students
        //    //    //.Where(s => !s.IsDeleted)
        //    //    .AsQueryable();



        //    //if (!string.IsNullOrWhiteSpace(search))
        //    //{
        //    //    query = query.Where(s =>
        //    //        s.FullName.Contains(search));
        //    //}



        //    //if (!string.IsNullOrWhiteSpace(email))
        //    //{
        //    //    query = query.Where(s =>
        //    //        s.Email.Contains(email));
        //    //}







        //    //if (!string.IsNullOrWhiteSpace(sortBy))
        //    //{
        //    //    switch (sortBy.ToLower())
        //    //    {
        //    //        case "name":

        //    //            query = descending
        //    //                ? query.OrderByDescending(s => s.FullName)
        //    //                : query.OrderBy(s => s.FullName);

        //    //            break;


        //    //        case "email":

        //    //            query = descending
        //    //                ? query.OrderByDescending(s => s.Email)
        //    //                : query.OrderBy(s => s.Email);

        //    //            break;


        //    //        case "date":

        //    //            query = descending
        //    //                ? query.OrderByDescending(s => s.EnrollmentDate)
        //    //                : query.OrderBy(s => s.EnrollmentDate);

        //    //            break;


        //    //        default:

        //    //            query = query.OrderBy(s => s.Id);

        //    //            break;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    query = query.OrderBy(s => s.Id);
        //    //}


        //    //// =========================
        //    //// 6. Pagination
        //    //// =========================

        //    //var students = await query
        //    //    .Skip((page - 1) * pageSize)
        //    //    .Take(pageSize)
        //    //    .Select(s => new StudentSearchDto
        //    //    {
        //    //        //Id = s.Id,
        //    //        FullName = s.FullName,
        //    //        Email = s.Email,
        //    //        Phone = s.Phone,
        //    //        EnrollmentDate = s.EnrollmentDate
        //    //    })
        //    //    .ToListAsync();


        //    //// =========================
        //    //// 7. Calculate Total Pages
        //    //// =========================
        //    //var totalCount = await query.CountAsync();

        //    //var totalPages = (int)Math.Ceiling(
        //    //    (double)totalCount / pageSize);


        //    //// =========================
        //    //// 8. Response
        //    //// =========================

        //    //var result = new PagedResultDto<StudentSearchDto>
        //    //{
        //    //    Page = page,
        //    //    PageSize = pageSize,
        //    //    TotalCount = totalCount,
        //    //    TotalPages = totalPages,
        //    //    Data = students
        //    //};
        //    #endregion


        //    var result = await _studentService.SearchStudents(search, email, page, pageSize, sortBy, descending);


        //    return Ok( result);
        //}



    }
}

