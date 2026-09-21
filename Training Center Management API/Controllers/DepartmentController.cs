using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Center_Management_API.Dtos.Department;
using Training_Center_Management_API.Services.Department;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();

            return Ok(departments);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department =  await _departmentService.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDepartmentDto dto)
        {
            var department = await _departmentService.CreateAsync(dto);

            if (department == null)
            {
                return BadRequest("Department name already exists.");
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = department.Id },
                department);
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDto dto)
        {
            var result = await _departmentService.UpdateAsync(id, dto);

            if (!result)
            {
                return BadRequest("Department not found or name already exists.");
            }

            return NoContent();
        }





        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _departmentService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(
                    "Department not found or contains courses.");
            }

            return NoContent();
        }


    }
}
