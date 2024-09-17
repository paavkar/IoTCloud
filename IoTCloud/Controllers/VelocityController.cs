using IoTCloud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelocityController(IReadingsService readingsService, IUserService userService) : ControllerBase
    {
        [HttpGet("add")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, float velocity)
        {
            if (apiKey.IsNullOrEmpty() || sensorName.IsNullOrEmpty()) return BadRequest("Missing apiKey or sensorName");

            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddVelocityReading(velocity, sensorName, existingKey.UserId, DateTimeOffset.Now);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }

        [HttpGet("addBinary")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, int binary)
        {
            if (apiKey.IsNullOrEmpty() || sensorName.IsNullOrEmpty()) return BadRequest("Missing apiKey or sensorName");

            if (binary > 1 || binary < 0) return BadRequest("Invalid value given.");

            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddBinaryReading(binary, sensorName, existingKey.UserId, DateTimeOffset.Now, ReadingType.Velocity);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }
    }
}
