using Microsoft.AspNetCore.Mvc;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.BusinessLocation;

namespace Weinmann.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessLocationController : ControllerBase
    {
        private readonly IBusinessLocationService _businessLocationService;

        public BusinessLocationController(IBusinessLocationService businessLocationService)
        {
            _businessLocationService = businessLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> ListBusinessLocations()
        {
            var result = await _businessLocationService.ListBusinessLocations();

            return Ok(result);
        }

        // GET api/<BusinessLocationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessLocationById(int id)
        {
            var result = await _businessLocationService.GetBusinessLocationById(id);

            return Ok(result);
        }

        // POST api/<BusinessLocationController>
        [HttpPost]
        public async Task<IActionResult> CreateBusinessLocation([FromBody] UpdateBusinessLocationDTO createBusinessLocationDTO)
        {
            var result = await _businessLocationService.CreateBusinessLocation(createBusinessLocationDTO);

            return Ok(result);
        }

        // PUT api/<BusinessLocationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusinessLocation(int businessLocationId, [FromBody] UpdateBusinessLocationDTO updateBusinessLocationDTO)
        {
            var result = await _businessLocationService.UpdateBusinessLocation(businessLocationId, updateBusinessLocationDTO);

            return Ok(result);
        }

        // DELETE api/<BusinessLocationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessLocation(int id)
        {
            await _businessLocationService.RemoveBusinessLocation(id);

            return Ok();
        }
    }
}
