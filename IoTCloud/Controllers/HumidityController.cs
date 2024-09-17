﻿using IoTCloud.Services;
using Microsoft.AspNetCore.Mvc;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumidityController(IReadingsService readingsService, IUserService userService) : ControllerBase
    {
        [HttpGet("add")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, float humidity)
        {
            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddHumidityReading(humidity, sensorName, existingKey.UserId, DateTime.Now);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }

        [HttpGet("addBinary")]
        public async Task<IActionResult> AddReading(string apiKey, string sensorName, int binary)
        {
            if (binary > 1 || binary < 0) return BadRequest("Invalid value given.");
            var existingKey = await userService.CheckApiKeyExistsAsync(apiKey);

            if (existingKey is null) return Unauthorized("API key is invalid.");

            var isOperationSuccessful = await readingsService.AddBinaryReading(binary, sensorName, existingKey.UserId, DateTime.Now, ReadingType.Humidity);

            if (!isOperationSuccessful)
            {
                return BadRequest("There was an error saving the reading. Check your inputs.");
            }
            return Ok();
        }
    }
}