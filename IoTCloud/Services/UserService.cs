using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace IoTCloud.Services
{
    public class UserService(ApplicationDbContext context, IConfiguration configuration) : IUserService
    {
        private SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey)
        {
            apiKey = apiKey.Replace(" ", "+");
            var existingKey = await context.ApiKeys.FirstOrDefaultAsync(ak => ak.ApiKeyId == apiKey);

            return existingKey;
        }

        public async Task<bool> DeleteUserApiKeyAsync(string apiKey)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
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

        public async Task<string?> GetUserApiKey(string userId)
        {
            var userKey = await context.ApiKeys.FirstOrDefaultAsync(ak => ak.UserId == userId);

            if (userKey != null) return userKey.ApiKeyId;

            return null;
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
            var emailNotifications = await context.EmailNotifications.Where(en => en.UserId == userId).ToListAsync();

            return emailNotifications;
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
    }
}
