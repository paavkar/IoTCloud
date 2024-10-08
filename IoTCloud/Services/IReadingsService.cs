﻿using IoTCloud.Models;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Services
{
    public interface IReadingsService
    {
        Task<bool> AddDistanceReading(float distance, string sensorName, string userId, DateTimeOffset timeOfMeasurement);
        Task<List<DistanceReading>> GetDistanceReadings(string userId);
        Task<bool> RemoveDistanceReadings(string userId, string sensorName);

        Task<bool> AddLuminosityReading(float luminosity, string sensorName, string userId, DateTimeOffset timeOfMeasurement);
        Task<List<LuminosityReading>> GetLuminosityReadings(string userId);
        Task<bool> RemoveLuminosityReadings(string userId, string sensorName);

        Task<bool> AddTemperatureReading(float temperature, string sensorName, string userId, DateTimeOffset timeOfMeasurement);
        Task<List<TemperatureReading>> GetTemperatureReadings(string userId);
        Task<bool> RemoveTemperatureReadings(string userId, string sensorName);

        Task<bool> AddVelocityReading(float velocity, string sensorName, string userId, DateTimeOffset timeOfMeasurement);
        Task<List<VelocityReading>> GetVelocityReadings(string userId);
        Task<bool> RemoveVelocityReadings(string userId, string sensorName);

        Task<bool> AddBinaryReading(int binary, string sensorName, string userId, DateTimeOffset timeOfMeasurement, ReadingType readingType);
        Task<List<BinaryReading>> GetBinaryReadings(string userId);
        Task<bool> RemoveBinaryReadings(string userId, ReadingType readingType, string sensorName);

        Task<bool> AddHumidityReading(float humidity, string sensorName, string userId, DateTimeOffset timeOfMeasurement);
        Task<List<HumidityReading>> GetHumidityReadings(string userId);
        Task<bool> RemoveHumidityReadings(string userId, string sensorName);
    }
}
