using IoTCloud.Models;
using Microsoft.Data.SqlClient;

namespace IoTCloud.Services
{
    public interface IUserService
    {
        Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey);
        Task<string> GetUserApiKey(string userId);
        Task<ApiKey> SetUserApiKey(string userId);
        Task<bool> DeleteUserApiKeyAsync(string apiKey, string userId);
        Task<bool> AddEmailNotification(EmailNotification emailNotification);
        Task<List<EmailNotification>> GetEmailNotifications(string userId);
        Task<bool> RemoveEmailNotification(string id);
        Task<EmailNotification> EditEmailNotification(EmailNotification emailNotification);

        Task DeleteNotificationsBySensor(string sensorName, string userId, SqlConnection connection, SqlTransaction transaction, bool deleteBySensor = false);
        Task DeleteReadingsBySensor(string sensorName, string userId, SqlConnection connection, SqlTransaction transaction, bool deleteBySensor = false);
    }
}
