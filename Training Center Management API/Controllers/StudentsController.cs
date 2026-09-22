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



        

        [Authorize (Roles ="Admin,Instructor" ) ]
        [HttpGet ("SearchStudent") ]
        public async Task<ActionResult<PagedResultDto<StudentSearchDto>>> SearchStudents([FromQuery]StudentQueryDto dto)    //?
        {
            var result = await _studentService.SearchStudents(dto);


            return Ok(result);

        }


       


        [Authorize(Roles = "Admin")]
        [HttpPut("EditRoleToInstructor/{studentId}")]
        public async Task<IActionResult> EditRoleToInstructor(int studentId)
        {
            var result = await _studentService.EditRoleToInstructor(studentId);

            if (!result)
            {
                return NotFound("Student not found or role change failed.");
            }

            return Ok("Student role changed to Instructor successfully.");
        }

    }
}

