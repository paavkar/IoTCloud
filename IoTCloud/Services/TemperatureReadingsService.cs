using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.EntityFrameworkCore;

namespace IoTCloud.Services
{
    public class TemperatureReadingsService : ITemperatureReadingsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TemperatureReadingsService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AddReading(float temperature, string userId, DateTime timeOfMeasurement)
        {
            TemperatureReading temperatureReading = new() { Temperature = temperature, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            _context.TemperatureReadings.Add(temperatureReading);
            var countOfSavedEntries = await _context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            return true;
        }

        public async Task<List<TemperatureReading>> GetTemperatureReadings(string userId)
        {
            var readings = await _context.TemperatureReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }
    }
}
