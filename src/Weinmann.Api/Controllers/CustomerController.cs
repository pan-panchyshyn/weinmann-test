using Microsoft.AspNetCore.Mvc;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.Customer;

namespace Weinmann.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> ListCustomers()
        {
            var result = await _customerService.ListCustomers();
            return Ok(result);
        }

        // GET api/<BusinessLocationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _customerService.GetCustomerById(id);
            return Ok(result);
        }

        // PUT api/<BusinessLocationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            var result = await _customerService.UpdateCustomer(customerId, updateCustomerDTO);
            return Ok(result);
        }

        // DELETE api/<BusinessLocationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.RemoveCustomer(id);
            return Ok();
        }
    }
}
