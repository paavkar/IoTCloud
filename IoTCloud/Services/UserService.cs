using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace IoTCloud.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey)
        {
            Console.WriteLine(apiKey);
            var existingKey = await _context.ApiKeys.FirstOrDefaultAsync(ak => ak.ApiKeyId == apiKey);

            return existingKey;
        }

        public async Task<string?> GetUserApiKey(string userId)
        {
            var userKey = await _context.ApiKeys.FirstOrDefaultAsync(ak => ak.UserId == userId);

            if (userKey != null) return userKey.ApiKeyId;

            return null;
        }

        public async Task<ApiKey> SetUserApiKey(string userId)
        {
            var userKey = await _context.ApiKeys.FirstOrDefaultAsync(ak => ak.UserId == userId);

            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);

            if (userKey is null)
            {
                userKey = new();
                userKey.UserId = userId;
                userKey.ApiKeyId = apiKey;
                _context.ApiKeys.Add(userKey);
            }
            else userKey.ApiKeyId = apiKey;

            await _context.SaveChangesAsync();

            return userKey;
        }
    }
}
