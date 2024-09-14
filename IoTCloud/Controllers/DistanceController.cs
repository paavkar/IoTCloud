using IoTCloud.Services;
using Microsoft.AspNetCore.Mvc;

namespace IoTCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController(IReadingsService readingsService, IUserService userService) : ControllerBase
    {
        [HttpGet("add")]
        public async Task<IActionResult> AddReading(string apiKey, float distance)
        {
            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddDistanceReading(distance, existingKey.UserId, DateTime.Now);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }
    }
}
