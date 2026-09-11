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
        //private readonly AppDbContext _context;                        //

        //public StudentsController(AppDbContext context)
        //{
        //    _context = context;
        //}     


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


        [Authorize (Policy = "StudentOwner")]          /////////غلط 
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentByID(int id)   // اي الفرق
        {

            #region gogogaga
            //var student = await _context.Students
            //    .FirstOrDefaultAsync(s => s.Id == id);

            //if (student == null)
            //{
            //    return NotFound();
            //}

            //return Ok(student);

            //if (id < 1)
            //{
            //return BadRequest($"Not accepted ID {id}");
            //}



            //var student = await _context.Students
            //    .Where(s => s.Id == id && !s.IsDeleted)
            //    .Select(s => new StudentDto
            //    {
            //        //Id = s.Id,
            //        FullName = s.FullName,
            //        Email = s.Email,
            //        Phone = s.Phone,
            //        BirthDate = s.BirthDate,
            //        Address = s.Address,
            //        EnrollmentDate = s.EnrollmentDate
            //    })
            //    .FirstOrDefaultAsync();

            #endregion

            var student = _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);


            //var authResult = await authorizationService.AuthorizeAsync(User, id, "StudentOwner");

            //if (!authResult.Succeeded)
            //{
            //    return Forbid(); // 403
            //}

            //return Ok(student);


        }





        [Authorize (Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto dto)
        {
            #region gogogo
            //using var transaction = await _context.Database.BeginTransactionAsync();

            //try
            //{


            //    var student = new Student
            //    {
            //        FullName = dto.FullName,
            //        Email = dto.Email,
            //        Phone = dto.Phone,
            //        BirthDate = dto.BirthDate,
            //        Address = dto.Address,

            //        EnrollmentDate = DateTime.UtcNow,

            //        CreatedAt = DateTime.UtcNow,
            //        CreatedBy = "system",
            //        IsDeleted = false
            //    };

            //    _context.Students.Add(student);

            //    await _context.SaveChangesAsync();

            //    await transaction.CommitAsync();


            //var result = new StudentDto                    //  عملنا result علشان نرجع ال enrullmentdata
            //{

            //    FullName = student.FullName,
            //    Email = student.Email,
            //    Phone = student.Phone,
            //    BirthDate = student.BirthDate,
            //    Address = student.Address,
            //    EnrollmentDate = student.EnrollmentDate
            //};
            #endregion

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
            #region kukhg
            //if (id < 1)
            //{
            //    return BadRequest($"Not accepted ID {id}");
            //}


            //var student = await _context.Students
            //    .FirstOrDefaultAsync(s =>
            //        s.Id == id &&
            //        !s.IsDeleted);

            //if (student == null)
            //    return NotFound();

            //student.FullName = dto.FullName;
            //student.Email = dto.Email;
            //student.Phone = dto.Phone;
            //student.BirthDate = dto.BirthDate;
            //student.Address = dto.Address;

            //student.UpdatedAt = DateTime.UtcNow;
            //student.UpdatedBy = "system";

            //await _context.SaveChangesAsync();
            #endregion

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
            #region hgfdjhfg
            //if (id < 1)
            //{
            //    return BadRequest($"Not accepted ID {id}");
            //}


            //var student = await _context.Students
            //    .FirstOrDefaultAsync(s =>
            //        s.Id == id &&
            //        !s.IsDeleted);

            //if (student == null)
            //    return NotFound();

            //// Soft Delete
            //student.IsDeleted = true;
            //student.UpdatedAt = DateTime.UtcNow;
            //student.UpdatedBy = "system";

            //await _context.SaveChangesAsync();
            #endregion

            if(!await _studentService.DeleteAsync(id))
            {
                return NotFound();
            }

            return NoContent();
        }



        /////////////////////////////////////////////////////  top
        ////////////////////////////////////////////////////
        ///////////////////////////////////////////////////
        ////[AllowAnonymous]
        //[Authorize(Roles = "Admin,Instructor")]
        //[HttpGet("GetTop")]
        //public async Task<ActionResult> GetTop()
        //{

        //    var students = await _context.Enrollments
        //        //.Where(e => !e.IsDeleted)

        //        .GroupBy(e => new
        //        {
        //            e.StudentId,
        //            e.Student.FullName,
        //        })
        //        .Select(g => new
        //        {
        //            FullName = g.Key.FullName,
        //            AverageGrade = g.Average(e => e.Grade)
        //        })
        //        .OrderByDescending(x => x.AverageGrade)
        //        .Take(3)
        //        .ToListAsync();

        //    return Ok(students);








        //var student = await _context.Students.Where(s => !s.IsDeleted)
        //    .Select(s => new
        //    {
        //        fullname = s.FullName,
        //        AverageGrade = s.Enrollments
        //    .Where(e => !e.IsDeleted)
        //    .Average(e => e.Grade)
        //    })
        //    .OrderByDescending(s => s.AverageGrade)
        //    .Take(3)
        //    .ToListAsync();

        //return Ok(student);
        //} 


      



        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/details")]
        public async Task<ActionResult<StudentDetailsDto>> GetStudentDetails(int id)
        {
            #region ehfgf
            //var student = await _context.Students
            //    .Where(s => s.Id == id && !s.IsDeleted)
            //    .Select(s => new StudentDetailsDto
            //    {
            //        //Id = s.Id,
            //        FullName = s.FullName,
            //        Email = s.Email,
            //        EnrollmentDate = s.EnrollmentDate,

            //        Enrollments = s.Enrollments
            //            .Select(e => new EnrollmentDto
            //            {
            //                //CourseId = e.CourseId,
            //                CourseName = e.Course.Name,
            //                Grade = e.Grade,
            //                Status = e.Status,
            //                EnrollmentDate = e.EnrollmentDate
            //            })
            //            .ToList(),

            //        Payments = s.Payments
            //            .Select(p => new PaymentDto
            //            {
            //                Amount = p.Amount,
            //                PaymentDate = p.PaymentDate,
            //                PaymentMethod = p.PaymentMethod
            //            })
            //            .ToList(),

            //        Certificates = s.Certificates
            //            .Select(c => new CertificateDto
            //            {
            //                CertificateNumber = c.CertificateNumber,
            //                CourseName = c.Course.Name,
            //                IssueDate = c.IssueDate,
            //                Grade = c.Grade
            //            })
            //            .ToList()
            //    })
            //    .FirstOrDefaultAsync();

            #endregion

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
        public async Task<ActionResult<PagedResultDto<StudentSearchDto>>> SearchStudents([FromBody]StudentQueryDto dto)
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

