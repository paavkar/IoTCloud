using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace IoTCloud.Services
{
    public class UserService(ApplicationDbContext context, IConfiguration configuration, IGraphsService graphsService) : IUserService
    {
        private SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey)
        {
            try
            {
                apiKey = apiKey.Replace(" ", "+");
                var existingKey = await context.ApiKeys.FirstOrDefaultAsync(ak => ak.ApiKeyId == apiKey);

                return existingKey;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserApiKeyAsync(string apiKey, string userId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await DeleteReadingsBySensor("", userId, connection, transaction);

                        await graphsService.DeleteGraphsBySensor("", userId, connection, transaction);

                        await DeleteNotificationsBySensor("", userId, connection, transaction);

                        var removeSensorsSql = @"
                                               DELETE s
                                               FROM Sensors s
                                               WHERE s.ApiKey = @ApiKey";

                        var affectedRows = await connection.ExecuteAsync(removeSensorsSql, new { ApiKey = apiKey }, transaction);

                        var sql = @"
                                  DELETE ak
                                  FROM ApiKeys ak
                                  WHERE ak.ApiKeyId = @ApiKey";

                        var affected = await connection.ExecuteAsync(sql, new { ApiKey = apiKey }, transaction);

                        transaction.Commit();

                        return affected > 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<string> GetUserApiKey(string userId)
        {
            try
            {
                var userKey = await context.ApiKeys.FirstOrDefaultAsync(ak => ak.UserId == userId);

                if (userKey != null) return userKey.ApiKeyId;

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<ApiKey> SetUserApiKey(string userId)
        {
            var userKey = await context.ApiKeys.FirstOrDefaultAsync(ak => ak.UserId == userId);

            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);

            if (userKey is null)
            {
                userKey = new();
                userKey.UserId = userId;
                userKey.ApiKeyId = apiKey;
                context.ApiKeys.Add(userKey);
            }
            else userKey.ApiKeyId = apiKey;

            await context.SaveChangesAsync();

            return userKey;
        }

        public async Task<bool> AddEmailNotification(EmailNotification emailNotification)
        {
            if (emailNotification == null) return false;

            context.EmailNotifications.Add(emailNotification);
            var addedCount = await context.SaveChangesAsync();

            return addedCount > 0;
        }

        public async Task<List<EmailNotification>> GetEmailNotifications(string userId)
        {
            try
            {
                var emailNotifications = await context.EmailNotifications.Where(en => en.UserId == userId).ToListAsync();

                return emailNotifications;
            }
            catch (Exception)
            {
                return new List<EmailNotification>();
            }
        }

        public async Task<bool> RemoveEmailNotification(string id)
        {
            using var connection = GetConnection();

            var sql = @"
                      DELETE en
                      FROM EmailNotifications en
                      WHERE en.Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }

        public async Task<EmailNotification> EditEmailNotification(EmailNotification emailNotification)
        {
            var dbNotification = await context.EmailNotifications.FindAsync(emailNotification.Id);

            if (dbNotification is not null)
            {
                dbNotification.Email = emailNotification.Email;
                dbNotification.ThresholdValue = emailNotification.ThresholdValue;
                dbNotification.NotificationMessage = emailNotification.NotificationMessage;
                dbNotification.NotificationThreshold = emailNotification.NotificationThreshold;

                await context.SaveChangesAsync();
            }
            return dbNotification;
        }

        public async Task DeleteNotificationsBySensor(string sensorName, string userId, SqlConnection connection, SqlTransaction transaction, bool deleteBySensor = false)
        {
            var removeEmailNotificationSql = @"
                                             DELETE en
                                             FROM EmailNotifications en
                                             WHERE en.UserId = @UserId";

            if (deleteBySensor)
            {
                removeEmailNotificationSql += " AND en.SensorName = @SensorName";
                await connection.ExecuteAsync(removeEmailNotificationSql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeEmailNotificationSql, new { UserId = userId }, transaction);
        }

        public async Task DeleteReadingsBySensor(string sensorName, string userId, SqlConnection connection, SqlTransaction transaction, bool deleteBySensor = false)
        {
            var removeDistanceSql = @"
                       DELETE dr
                       FROM DistanceReadings dr
                       WHERE dr.UserId = @UserId";

            if (deleteBySensor)
            {
                removeDistanceSql += " AND dr.SensorName = @SensorName";
                await connection.ExecuteAsync(removeDistanceSql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeDistanceSql, new { UserId = userId }, transaction);


            var removeLuminositySql = @"
                       DELETE lr
                       FROM LuminosityReadings lr
                       WHERE lr.UserId = @UserId";

            if (deleteBySensor)
            {
                removeLuminositySql += " AND lr.SensorName = @SensorName";
                await connection.ExecuteAsync(removeLuminositySql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeLuminositySql, new { UserId = userId }, transaction);


            var removeTemperatureSql = @"
                        DELETE tr
                        FROM TemperatureReadings tr
                        WHERE tr.UserId = @UserId";

            if (deleteBySensor)
            {
                removeTemperatureSql += " AND tr.SensorName = @SensorName";
                await connection.ExecuteAsync(removeTemperatureSql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeTemperatureSql, new { UserId = userId }, transaction);


            var removeVelocitySql = @"
                       DELETE vr
                       FROM VelocityReadings vr
                       WHERE vr.UserId = @UserId";

            if (deleteBySensor)
            {
                removeVelocitySql += " AND vr.SensorName = @SensorName";
                await connection.ExecuteAsync(removeVelocitySql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeVelocitySql, new { UserId = userId }, transaction);


            var removeBinarySql = @"
                       DELETE br
                       FROM BinaryReadings br
                       WHERE br.UserId = @UserId";

            if (deleteBySensor)
            {
                removeBinarySql += " AND br.SensorName = @SensorName";
                await connection.ExecuteAsync(removeBinarySql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeBinarySql, new { UserId = userId }, transaction);


            var removeHumiditySql = @"
                       DELETE hr
                       FROM HumidityReadings hr
                       WHERE hr.UserId = @UserId";

            if (deleteBySensor)
            {
                removeHumiditySql += " AND hr.SensorName = @SensorName";
                await connection.ExecuteAsync(removeHumiditySql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeHumiditySql, new { UserId = userId }, transaction);
        }
    }
}
