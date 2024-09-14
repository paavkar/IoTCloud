using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface IReadingsService
    {
        Task<bool> AddDistanceReading(float distance, string userId, DateTime timeOfMeasurement);
        Task<List<DistanceReading>> GetDistanceReadings(string userId);

        Task<bool> AddLuminosityReading(float luminosity, string userId, DateTime timeOfMeasurement);
        Task<List<LuminosityReading>> GetLuminosityReadings(string userId);

        Task<bool> AddTemperatureReading(float temperature, string userId, DateTime timeOfMeasurement);
        Task<List<TemperatureReading>> GetTemperatureReadings(string userId);


        Task<bool> AddVelocityReading(float velocity, string userId, DateTime timeOfMeasurement);
        Task<List<VelocityReading>> GetVelocityReadings(string userId);
    }
}
