﻿using IoTCloud.Services;
using Microsoft.AspNetCore.Mvc;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController(IReadingsService readingsService, IUserService userService) : ControllerBase
    {
        [HttpGet("add")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, float temperature)
        {
            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddTemperatureReading(temperature, sensorName, existingKey.UserId, DateTime.Now);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }

        [HttpGet("addBinary")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, int binary)
        {
            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddBinaryReading(binary, sensorName, existingKey.UserId, DateTime.Now, ReadingType.Temperature);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }
    }
}
