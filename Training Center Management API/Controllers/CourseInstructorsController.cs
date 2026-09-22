using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Dtos.CourseInstructor;
using Training_Center_Management_API.Services.CourseInstructorService;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin") ]
    public class CourseInstructorsController : ControllerBase
    {

        private readonly ICourseInstructorService _courseInstructorService;

        public CourseInstructorsController( ICourseInstructorService courseInstructorService)
        {
            _courseInstructorService = courseInstructorService;
        }




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseInstructorService.GetAllAsync();

            return Ok(result);
        }





        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInstructorDto dto)
        {
            var result =await _courseInstructorService.CreateAsync(dto);

            if (result == null)
            {
                return BadRequest("Course or Instructor does not exist, or assignment already exists.");
            }

            return Ok(result);
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseInstructorService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
