using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.EntityFrameworkCore;

namespace IoTCloud.Services
{
    public class ReadingsService : IReadingsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ReadingsService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AddDistanceReading(float distance, string userId, DateTime timeOfMeasurement)
        {
            DistanceReading distanceReading = new() { Distance = distance, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            _context.DistanceReadings.Add(distanceReading);
            var countOfSavedEntries = await _context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            return true;
        }

        public async Task<bool> AddLuminosityReading(float luminosity, string userId, DateTime timeOfMeasurement)
        {
            LuminosityReading luminosityReading = new() { Luminosity = luminosity, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            _context.LuminosityReadings.Add(luminosityReading);
            var countOfSavedEntries = await _context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            return true;
        }

        public async Task<bool> AddTemperatureReading(float temperature, string userId, DateTime timeOfMeasurement)
        {
            TemperatureReading temperatureReading = new() { Temperature = temperature, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            _context.TemperatureReadings.Add(temperatureReading);
            var countOfSavedEntries = await _context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            return true;
        }

        public async Task<bool> AddVelocityReading(float velocity, string userId, DateTime timeOfMeasurement)
        {
            VelocityReading velocityReading = new() { Velocity = velocity, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            _context.VelocityReadings.Add(velocityReading);
            var countOfSavedEntries = await _context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            return true;
        }

        public async Task<List<DistanceReading>> GetDistanceReadings(string userId)
        {
            var readings = await _context.DistanceReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<List<LuminosityReading>> GetLuminosityReadings(string userId)
        {
            var readings = await _context.LuminosityReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<List<TemperatureReading>> GetTemperatureReadings(string userId)
        {
            var readings = await _context.TemperatureReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<List<VelocityReading>> GetVelocityReadings(string userId)
        {
            var readings = await _context.VelocityReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }
    }
}
