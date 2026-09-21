using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Dtos.Enrollment;
using Training_Center_Management_API.Services.Enrollment;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EnrollmentDto>> GetAll()
        {
            var enrollments =
                await _enrollmentService.GetAllAsync();

            return Ok(enrollments);
        }



        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollment =
                await _enrollmentService.GetByIdAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }




        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEnrollmentDto dto)
        {
            var enrollment =
                await _enrollmentService.CreateAsync(dto);

            if (enrollment == null)
            {
                return BadRequest(
                    "Student or Course does not exist, " +
                    "or student is already enrolled.");
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = enrollment.Id },
                enrollment);
        }




        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            UpdateEnrollmentDto dto)
        {
            var result =
                await _enrollmentService.UpdateAsync(
                    id,
                    dto);

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
            var result =
                await _enrollmentService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
