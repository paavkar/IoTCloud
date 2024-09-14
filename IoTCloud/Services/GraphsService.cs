using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;

namespace IoTCloud.Services
{
    public class GraphsService : IGraphsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public GraphsService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddUserGraph(GraphItem item, string userId)
        {
            await _context.GraphItems.AddAsync(item);
            await _context.SaveChangesAsync();

            using var connection = GetConnection();

            string insertSql = @"
                                INSERT INTO UserGraphItems (GraphId, UserId)
                                VALUES (@GraphId, @UserId)";

            await connection.ExecuteAsync(insertSql, new { GraphId = item.Id, UserId = userId });

            return true;
        }

        public async Task<List<GraphItem>> GetUserGraphs(string userId)
        {
            var sql = @"
                        SELECT gi.*
                        FROM GraphItems gi
                        LEFT JOIN UserGraphItems ugi ON gi.Id = ugi.GraphId
                        LEFT JOIN AspNetUsers u ON ugi.UserId = u.Id
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
                        LEFT JOIN UserGraphItems ugi ON gi.Id = ugi.GraphId
                        LEFT JOIN AspNetUsers u ON ugi.UserId = u.Id
                        WHERE u.Id = @UserId";

            var affected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return affected > 0 ? true : false;
        }

        public async Task<bool> DeleteGraph(string id)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE gi
                        FROM GraphItems gi
                        WHERE gi.Id = @Id";

            var affected = await connection.ExecuteAsync(sql, new { Id = id });

            return affected > 0 ? true : false;
        }
    }
}
