using IoTCloud.Models;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Services
{
    public interface IReadingsService
    {
        Task<bool> AddDistanceReading(float distance, string sensorName, string userId, DateTime timeOfMeasurement);
        Task<List<DistanceReading>> GetDistanceReadings(string userId);
        Task<bool> RemoveDistanceReadings(string userId);

        Task<bool> AddLuminosityReading(float luminosity, string sensorName, string userId, DateTime timeOfMeasurement);
        Task<List<LuminosityReading>> GetLuminosityReadings(string userId);
        Task<bool> RemoveLuminosityReadings(string userId);

        Task<bool> AddTemperatureReading(float temperature, string sensorName, string userId, DateTime timeOfMeasurement);
        Task<List<TemperatureReading>> GetTemperatureReadings(string userId);
        Task<bool> RemoveTemperatureReadings(string userId);


        Task<bool> AddVelocityReading(float velocity, string sensorName, string userId, DateTime timeOfMeasurement);
        Task<List<VelocityReading>> GetVelocityReadings(string userId);
        Task<bool> RemoveVelocityReadings(string userId);

        Task<bool> AddBinaryReading(int binary, string sensorName, string userId, DateTime timeOfMeasurement, ReadingType readingType);
        Task<List<BinaryReading>> GetBinaryReadings(string userId);
        Task<bool> RemoveBinaryReadings(string userId, ReadingType readingType);

        Task<bool> AddHumidityReading(float humidity, string sensorName, string userId, DateTime timeOfMeasurement);
        Task<List<HumidityReading>> GetHumidityReadings(string userId);
        Task<bool> RemoveHumidityReadings(string userId);
    }
}
