﻿using IoTCloud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController(IReadingsService readingsService, IUserService userService, ISensorsService sensorsService) : ControllerBase
    {
        [HttpGet("add")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, float distance)
        {
            if (apiKey.IsNullOrEmpty() || sensorName.IsNullOrEmpty()) return BadRequest("Missing apiKey or sensorName");

            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var sensorExists = await sensorsService.CheckSensorExists(sensorName.ToLower(), existingKey.UserId);

            if (!sensorExists) return BadRequest($"Sensor with the name {sensorName} does not exist");

            var isOperationSuccessful = await readingsService.AddDistanceReading(distance, sensorName, existingKey.UserId, DateTimeOffset.UtcNow);

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

            var sensorExists = await sensorsService.CheckSensorExists(sensorName.ToLower(), existingKey.UserId);

            if (!sensorExists) return BadRequest($"Sensor with the name {sensorName} does not exist");

            var isOperationSuccessful = await readingsService.AddBinaryReading(binary, sensorName.ToLower(), existingKey.UserId, DateTimeOffset.UtcNow, ReadingType.Distance);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }
    }
}
