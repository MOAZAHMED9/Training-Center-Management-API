using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Dtos.Instructors;
using Training_Center_Management_API.Services.Instrucrot;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstructorController : ControllerBase
    {

        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }





         
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instructors =
                await _instructorService.GetAllAsync();

            return Ok(instructors);
        }







        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }






        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create( CreateInstructorDto dto)
        {
            var instructor =
                await _instructorService.CreateAsync(dto);

            if (instructor == null)
            {
                return BadRequest("Email already exists.");
            }

            return CreatedAtAction(
                nameof(GetById),                              ////
                new { id = instructor.Id },
                instructor);
        }







        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateInstructorDto dto)
        {
            var result =
                await _instructorService.UpdateAsync(id, dto);

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
                await _instructorService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
