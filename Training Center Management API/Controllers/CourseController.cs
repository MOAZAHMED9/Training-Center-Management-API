using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Dtos.common;
using Training_Center_Management_API.Dtos.Courses;
using Training_Center_Management_API.Services.Course;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/Courses")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();

            return Ok(courses);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var course = await _courseService.CreateAsync(dto);

            if (course == null)
            {
                return BadRequest("Department does not exist.");
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.Id },
                course);
        }

        //[HttpPatch("{id}")]                                /////
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
        {
            var result =
                await _courseService.UpdateAsync(id, dto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("Details{id}")]                            // نخلي بالنا من المسافات علشان طلعت باج
        public async Task<ActionResult<CourseDetails>> CourseDetails(int id)
        {

            var details= await _courseService.CourseDetailsAsync(id);

            if (details == null)
            {
                return NotFound();
            }

            return details;

        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("Search")]
        public async Task<ActionResult<PagedResultDto<CourseDto>>> CourseSearch([FromQuery] CourseSearch search)
        {
            var result = await _courseService.CourseSearchAsync(search);
            return Ok(result);
        }

    }
}