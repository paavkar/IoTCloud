using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface IUserService
    {
        Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey);
        Task<string?> GetUserApiKey(string userId);
        Task<ApiKey> SetUserApiKey(string userId);
    }
}
