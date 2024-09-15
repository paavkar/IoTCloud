using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IoTCloud.Services
{
    public class ReadingsService(ApplicationDbContext context, IConfiguration configuration, IUserService userService, IEmailSender emailSender) : IReadingsService
    {
        private SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddDistanceReading(float distance, string userId, DateTime timeOfMeasurement)
        {
            DistanceReading distanceReading = new() { Distance = distance, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            context.DistanceReadings.Add(distanceReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotification = await userService.GetEmailNotification(userId);

            if (emailNotification.UserId == userId)
            {
                if (emailNotification.NotificationThreshold == Enums.Threshold.Under && distance <= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $" The sensor value was: {distance}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Distance notification", emailNotification.NotificationMessage);
                }
                else if (emailNotification.NotificationThreshold == Enums.Threshold.Over && distance >= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $" The sensor value was: {distance}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Distance notification", emailNotification.NotificationMessage);
                }
            }

            return true;
        }

        public async Task<bool> AddLuminosityReading(float luminosity, string userId, DateTime timeOfMeasurement)
        {
            LuminosityReading luminosityReading = new() { Luminosity = luminosity, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            context.LuminosityReadings.Add(luminosityReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotification = await userService.GetEmailNotification(userId);

            if (emailNotification.UserId == userId)
            {
                if (emailNotification.NotificationThreshold == Enums.Threshold.Under && luminosity <= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $" The sensor value was: {luminosity}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Luminosity notification", emailNotification.NotificationMessage);
                }
                else if (emailNotification.NotificationThreshold == Enums.Threshold.Over && luminosity >= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $" The sensor value was: {luminosity}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Luminosity notification", emailNotification.NotificationMessage);
                }
            }

            return true;
        }

        public async Task<bool> AddTemperatureReading(float temperature, string userId, DateTime timeOfMeasurement)
        {
            TemperatureReading temperatureReading = new() { Temperature = temperature, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            context.TemperatureReadings.Add(temperatureReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotification = await userService.GetEmailNotification(userId);

            if (emailNotification.UserId == userId)
            {
                if (emailNotification.NotificationThreshold == Enums.Threshold.Under && temperature <= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $"\n The sensor value was: {temperature}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Temperature notification", emailNotification.NotificationMessage);
                }
                else if (emailNotification.NotificationThreshold == Enums.Threshold.Over && temperature >= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $"\n The sensor value was: {temperature}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Temperature notification", emailNotification.NotificationMessage);
                }
            }

            return true;
        }

        public async Task<bool> AddVelocityReading(float velocity, string userId, DateTime timeOfMeasurement)
        {
            VelocityReading velocityReading = new() { Velocity = velocity, UserId = userId, TimeOfMeasurement = timeOfMeasurement };

            context.VelocityReadings.Add(velocityReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotification = await userService.GetEmailNotification(userId);

            if (emailNotification.UserId == userId)
            {
                if (emailNotification.NotificationThreshold == Enums.Threshold.Under && velocity <= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $" The sensor value was: {velocity}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Velocity notification", emailNotification.NotificationMessage);
                }
                else if (emailNotification.NotificationThreshold == Enums.Threshold.Over && velocity >= emailNotification.ThresholdValue)
                {
                    emailNotification.NotificationMessage += $" The sensor value was: {velocity}";
                    await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Velocity notification", emailNotification.NotificationMessage);
                }
            }

            return true;
        }

        public async Task<List<DistanceReading>> GetDistanceReadings(string userId)
        {
            var readings = await context.DistanceReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<List<LuminosityReading>> GetLuminosityReadings(string userId)
        {
            var readings = await context.LuminosityReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<List<TemperatureReading>> GetTemperatureReadings(string userId)
        {
            var readings = await context.TemperatureReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<List<VelocityReading>> GetVelocityReadings(string userId)
        {
            var readings = await context.VelocityReadings.Where(tr => tr.UserId == userId).ToListAsync();

            return readings;
        }

        public async Task<bool> RemoveDistanceReadings(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE dr
                        FROM DistanceReadings dr
                        WHERE dr.UserId = @UserId";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return affected > 0 ? true : false;
        }

        public async Task<bool> RemoveLuminosityReadings(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE lr
                        FROM LuminosityReadings lr
                        WHERE dr.UserId = @UserId";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return affected > 0 ? true : false;
        }

        public async Task<bool> RemoveTemperatureReadings(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE tr
                        FROM TemperatureReadings tr
                        WHERE dr.UserId = @UserId";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return affected > 0 ? true : false;
        }

        public async Task<bool> RemoveVelocityReadings(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE vr
                        FROM VelocityReadings vr
                        WHERE dr.UserId = @UserId";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return affected > 0 ? true : false;
        }
    }
}
