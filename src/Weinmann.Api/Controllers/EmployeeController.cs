using Microsoft.AspNetCore.Mvc;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.Employee;

namespace Weinmann.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> ListEmployees()
        {
            var result = await _employeeService.ListEmployees();

            return Ok(result);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);

            return Ok(result);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            var result = await _employeeService.CreateEmployee(updateEmployeeDTO);

            return Ok(result);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            var result = await _employeeService.UpdateEmployee(employeeId, updateEmployeeDTO);

            return Ok(result);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.RemoveEmployee(id);

            return Ok();
        }
    }
}
