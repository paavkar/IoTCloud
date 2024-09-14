using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;
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

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<ApiKey?> CheckApiKeyExistsAsync(string apiKey)
        {
            var existingKey = await _context.ApiKeys.FirstOrDefaultAsync(ak => ak.ApiKeyId == apiKey);

            return existingKey;
        }

        public async Task<bool> DeleteUserApiKeyAsync(string apiKey)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE ak
                        FROM ApiKeys ak
                        WHERE ak.ApiKeyId = @ApiKey";

            var affected = await connection.ExecuteAsync(sql, new { ApiKey = apiKey });

            return affected > 0 ? true : false;
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
