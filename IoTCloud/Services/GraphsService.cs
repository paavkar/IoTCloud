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
                        WHERE gi.UserId = @UserId";

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
                        WHERE gi.UserId = @UserId";

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

        public async Task<bool> AddUserTable(TableItem item)
        {
            await context.TableItems.AddAsync(item);
            var addedCount = await context.SaveChangesAsync();

            return addedCount > 0;
        }

        public async Task<List<TableItem>> GetUserTables(string userId)
        {
            var sql = @"
                        SELECT ti.*
                        FROM TableItems ti
                        WHERE ti.UserId = @UserId";

            using var connection = GetConnection();
            var tableItems = await connection.QueryAsync<TableItem>(sql, new { UserId = userId });

            return tableItems.ToList();
        }

        public async Task<bool> DeleteUserTables(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE ti
                        FROM TableItems ti
                        WHERE ti.UserId = @UserId";

            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTable(string id)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE ti
                        FROM TableItems ti
                        WHERE ti.Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }
    }
}
