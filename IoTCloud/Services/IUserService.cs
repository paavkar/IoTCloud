using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface IUserService
    {
        Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey);
        Task<string?> GetUserApiKey(string userId);
        Task<ApiKey> SetUserApiKey(string userId);
        Task<bool> DeleteUserApiKeyAsync(string apiKey);
        Task<bool> AddEmailNotification(EmailNotification emailNotification);
        Task<List<EmailNotification>> GetEmailNotifications(string userId);
        Task<bool> RemoveEmailNotification(string id);
    }
}
