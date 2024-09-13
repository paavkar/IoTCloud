using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface ITemperatureReadingsService
    {
        Task<bool> AddReading(float temperature, string userId, DateTime timeOfMeasurement);
        Task<List<TemperatureReading>> GetTemperatureReadings(string userId);
    }
}
