using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;

namespace IoTCloud.Services
{
    public class GraphsService(ApplicationDbContext context, IConfiguration configuration) : IGraphsService
    {
        private SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddUserGraph(GraphItem item)
        {
            await context.GraphItems.AddAsync(item);
            var addedCount = await context.SaveChangesAsync();

            return addedCount > 0;
        }

        public async Task<List<GraphItem>> GetUserGraphs(string userId)
        {
            var sql = @"
                        SELECT gi.*
                        FROM GraphItems gi
                        LEFT JOIN AspNetUsers u ON gi.UserId = u.Id
                        WHERE u.Id = @UserId";

            using var connection = GetConnection();
            var graphItems = await connection.QueryAsync<GraphItem>(sql, new { UserId = userId });

            return graphItems.ToList();
        }

        public async Task<bool> DeleteUserGraphs(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE gi
                        FROM GraphItems gi
                        LEFT JOIN AspNetUsers u ON gi.UserId = u.Id
                        WHERE u.Id = @UserId";

            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteGraph(string id)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE gi
                        FROM GraphItems gi
                        WHERE gi.Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }
    }
}
