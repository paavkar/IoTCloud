using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Services
{
    public class ReadingsService(ApplicationDbContext context, IConfiguration configuration, IUserService userService, IEmailSender emailSender) : IReadingsService
    {
        private SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddDistanceReading(float distance, string sensorName, string userId, DateTimeOffset timeOfMeasurement)
        {
            DistanceReading distanceReading = new() { Distance = distance, UserId = userId, TimeOfMeasurement = timeOfMeasurement, SensorName = sensorName };

            context.DistanceReadings.Add(distanceReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotifications = await userService.GetEmailNotifications(userId);

            foreach (var emailNotification in emailNotifications)
            {
                if (emailNotification.UserId == userId && emailNotification.ReadingType == ReadingType.Distance && !emailNotification.IsBinary && emailNotification.SensorName == sensorName)
                {
                    if (emailNotification.NotificationThreshold == Threshold.Under && distance <= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {distance}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Distance alert", emailNotification.NotificationMessage);
                    }
                    else if (emailNotification.NotificationThreshold == Threshold.Over && distance >= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {distance}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Distance alert", emailNotification.NotificationMessage);
                    }
                }
            }

            return true;
        }

        public async Task<bool> AddLuminosityReading(float luminosity, string sensorName, string userId, DateTimeOffset timeOfMeasurement)
        {
            LuminosityReading luminosityReading = new() { Luminosity = luminosity, UserId = userId, TimeOfMeasurement = timeOfMeasurement, SensorName = sensorName };

            context.LuminosityReadings.Add(luminosityReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotifications = await userService.GetEmailNotifications(userId);

            foreach (var emailNotification in emailNotifications)
            {
                if (emailNotification.UserId == userId && emailNotification.ReadingType == ReadingType.Luminosity && !emailNotification.IsBinary && emailNotification.SensorName == sensorName)
                {
                    if (emailNotification.NotificationThreshold == Threshold.Under && luminosity <= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {luminosity}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Luminosity alert", emailNotification.NotificationMessage);
                    }
                    else if (emailNotification.NotificationThreshold == Threshold.Over && luminosity >= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {luminosity}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Luminosity alert", emailNotification.NotificationMessage);
                    }
                }
            }

            return true;
        }

        public async Task<bool> AddTemperatureReading(float temperature, string sensorName, string userId, DateTimeOffset timeOfMeasurement)
        {
            TemperatureReading temperatureReading = new() { Temperature = temperature, UserId = userId, TimeOfMeasurement = timeOfMeasurement, SensorName = sensorName };

            context.TemperatureReadings.Add(temperatureReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotifications = await userService.GetEmailNotifications(userId);

            foreach (var emailNotification in emailNotifications)
            {
                if (emailNotification.UserId == userId && emailNotification.ReadingType == ReadingType.Temperature && !emailNotification.IsBinary && emailNotification.SensorName == sensorName)
                {
                    if (emailNotification.NotificationThreshold == Threshold.Under && temperature <= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {temperature}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Temperature alert", emailNotification.NotificationMessage);
                    }
                    else if (emailNotification.NotificationThreshold == Threshold.Over && temperature >= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {temperature}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Temperature alert", emailNotification.NotificationMessage);
                    }
                }
            }

            return true;
        }

        public async Task<bool> AddVelocityReading(float velocity, string sensorName, string userId, DateTimeOffset timeOfMeasurement)
        {
            VelocityReading velocityReading = new() { Velocity = velocity, UserId = userId, TimeOfMeasurement = timeOfMeasurement, SensorName = sensorName };

            context.VelocityReadings.Add(velocityReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotifications = await userService.GetEmailNotifications(userId);

            foreach (var emailNotification in emailNotifications)
            {
                if (emailNotification.UserId == userId && emailNotification.ReadingType == ReadingType.Velocity && !emailNotification.IsBinary && emailNotification.SensorName == sensorName)
                {
                    if (emailNotification.NotificationThreshold == Threshold.Under && velocity <= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {velocity}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Velocity alert", emailNotification.NotificationMessage);
                    }
                    else if (emailNotification.NotificationThreshold == Threshold.Over && velocity >= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {velocity}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Velocity alert", emailNotification.NotificationMessage);
                    }
                }
            }

            return true;
        }

        public async Task<bool> AddBinaryReading(int binary, string sensorName, string userId, DateTimeOffset timeOfMeasurement, ReadingType readingType)
        {
            BinaryReading binaryReading = new() { Binary = binary, UserId = userId, TimeOfMeasurement = timeOfMeasurement, ReadingType = readingType, SensorName = sensorName };

            context.BinaryReadings.Add(binaryReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotifications = await userService.GetEmailNotifications(userId);

            foreach (var emailNotification in emailNotifications)
            {
                if (emailNotification.UserId == userId && emailNotification.ReadingType == readingType && emailNotification.IsBinary && emailNotification.SensorName == sensorName)
                {
                    if (binary == 1)
                    {
                        emailNotification.NotificationMessage += $" This alert was for a {emailNotification.ReadingType} sensor.";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Binary notification", emailNotification.NotificationMessage);
                    }
                }
            }

            return true;
        }

        public async Task<bool> AddHumidityReading(float humidity, string sensorName, string userId, DateTimeOffset timeOfMeasurement)
        {
            HumidityReading humidityReading = new() { Humidity = humidity, UserId = userId, TimeOfMeasurement = timeOfMeasurement, SensorName = sensorName };

            context.HumidityReadings.Add(humidityReading);
            var countOfSavedEntries = await context.SaveChangesAsync();

            if (countOfSavedEntries < 1) return false;

            var emailNotifications = await userService.GetEmailNotifications(userId);

            foreach (var emailNotification in emailNotifications)
            {
                if (emailNotification.UserId == userId && emailNotification.ReadingType == ReadingType.Humidity && !emailNotification.IsBinary && emailNotification.SensorName == sensorName)
                {
                    if (emailNotification.NotificationThreshold == Threshold.Under && humidity <= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {humidity}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Humidity alert", emailNotification.NotificationMessage);
                    }
                    else if (emailNotification.NotificationThreshold == Threshold.Over && humidity >= emailNotification.ThresholdValue)
                    {
                        emailNotification.NotificationMessage += $" The sensor value was: {humidity}";
                        await emailSender.SendEmailAsync(emailNotification.Email, "IoTCloud - Humidity alert", emailNotification.NotificationMessage);
                    }
                }
            }

            return true;
        }

        public async Task<List<DistanceReading>> GetDistanceReadings(string userId)
        {
            var readings = await context.DistanceReadings.Where(tr => tr.UserId == userId).OrderByDescending(dr => dr.TimeOfMeasurement).ToListAsync();

            return readings;
        }

        public async Task<List<LuminosityReading>> GetLuminosityReadings(string userId)
        {
            var readings = await context.LuminosityReadings.Where(lr => lr.UserId == userId).OrderByDescending(lr => lr.TimeOfMeasurement).ToListAsync();

            return readings;
        }

        public async Task<List<TemperatureReading>> GetTemperatureReadings(string userId)
        {
            var readings = await context.TemperatureReadings.Where(tr => tr.UserId == userId).OrderByDescending(tr => tr.TimeOfMeasurement).ToListAsync();

            return readings;
        }

        public async Task<List<VelocityReading>> GetVelocityReadings(string userId)
        {
            var readings = await context.VelocityReadings.Where(vr => vr.UserId == userId).OrderByDescending(vr => vr.TimeOfMeasurement).ToListAsync();

            return readings;
        }

        public async Task<List<BinaryReading>> GetBinaryReadings(string userId)
        {
            var binaryReadings = await context.BinaryReadings.Where(br => br.UserId == userId).OrderByDescending(br => br.TimeOfMeasurement).ToListAsync();

            return binaryReadings;
        }

        public async Task<List<HumidityReading>> GetHumidityReadings(string userId)
        {
            var readings = await context.HumidityReadings.Where(hr => hr.UserId == userId).OrderByDescending(hr => hr.TimeOfMeasurement).ToListAsync();

            return readings;
        }

        public async Task<bool> RemoveDistanceReadings(string userId, string sensorName)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE dr
                        FROM DistanceReadings dr
                        WHERE dr.UserId = @UserId
                        AND dr.SensorName = @SensorName";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId, SensorName = sensorName });

            return affected > 0;
        }

        public async Task<bool> RemoveLuminosityReadings(string userId, string sensorName)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE lr
                        FROM LuminosityReadings lr
                        WHERE lr.UserId = @UserId
                        AND lr.SensorName = @SensorName";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId, SensorName = sensorName });

            return affected > 0;
        }

        public async Task<bool> RemoveTemperatureReadings(string userId, string sensorName)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE tr
                        FROM TemperatureReadings tr
                        WHERE tr.UserId = @UserId
                        AND tr.SensorName = @SensorName";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId, SensorName = sensorName });

            return affected > 0;
        }

        public async Task<bool> RemoveVelocityReadings(string userId, string sensorName)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE vr
                        FROM VelocityReadings vr
                        WHERE vr.UserId = @UserId
                        AND vr.SensorName = @SensorName";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId, SensorName = sensorName });

            return affected > 0;
        }

        public async Task<bool> RemoveBinaryReadings(string userId, ReadingType readingType, string sensorName)
        {
            var sql = @"
                        DELETE br
                        FROM BinaryReadings br
                        WHERE br.UserId = @UserId
                        AND br.ReadingType = @ReadingType
                        AND br.SensorName = @SensorName";

            using var connection = GetConnection();

            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId, ReadingType = readingType, SensorName = sensorName });

            return rowsAffected > 0;
        }

        public async Task<bool> RemoveHumidityReadings(string userId, string sensorName)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE hr
                        FROM HumidityReadings hr
                        WHERE hr.UserId = @UserId
                        AND hr.SensorName = @SensorName";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId, SensorName = sensorName });

            return affected > 0;
        }
    }
}
