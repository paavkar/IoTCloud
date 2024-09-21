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
            try
            {
                var sql = @"
                        SELECT gi.*
                        FROM GraphItems gi
                        WHERE gi.UserId = @UserId";

                using var connection = GetConnection();
                var graphItems = await connection.QueryAsync<GraphItem>(sql, new { UserId = userId });

                return graphItems.ToList();
            }
            catch (Exception)
            {
                return new List<GraphItem>();
            }
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
            try
            {
                var sql = @"
                        SELECT ti.*
                        FROM TableItems ti
                        WHERE ti.UserId = @UserId";

                using var connection = GetConnection();
                var tableItems = await connection.QueryAsync<TableItem>(sql, new { UserId = userId });

                return tableItems.ToList();
            }
            catch (Exception)
            {
                return new List<TableItem>();
            }
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

        public async Task<bool> AddUserBinaryGraph(BinaryGraphItem item)
        {
            await context.BinaryGraphItems.AddAsync(item);
            var addedCount = await context.SaveChangesAsync();

            return addedCount > 0;
        }

        public async Task<List<BinaryGraphItem>> GetUserBinaryGraphItems(string userId)
        {
            try
            {
                var sql = @"
                        SELECT bi.*
                        FROM BinaryGraphItems bi
                        WHERE bi.UserId = @UserId";

                using var connection = GetConnection();
                var tableItems = await connection.QueryAsync<BinaryGraphItem>(sql, new { UserId = userId });

                return tableItems.ToList();
            }
            catch (Exception)
            {
                return new List<BinaryGraphItem>();
            }
        }

        public async Task<bool> DeleteUserBinaryGraphs(string userId)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE bi
                        FROM BinaryGraphItems bi
                        WHERE bi.UserId = @UserId";

            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteBinaryGraph(string id)
        {
            using var connection = GetConnection();
            var sql = @"
                        DELETE bi
                        FROM BinaryGraphItems bi
                        WHERE bi.Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }

        public async Task DeleteGraphsBySensor(string sensorName, string userId, SqlConnection connection, SqlTransaction transaction, bool deleteBySensor = false)
        {
            var removeGraphsSql = @"
                                  DELETE gi
                                  FROM GraphItems gi
                                  WHERE gi.UserId = @UserId";

            if (deleteBySensor)
            {
                removeGraphsSql += " AND gi.SensorName = @SensorName";
                await connection.ExecuteAsync(removeGraphsSql, new { UserId = userId, SensorName = sensorName }, transaction);
            }
            else await connection.ExecuteAsync(removeGraphsSql, new { UserId = userId }, transaction);

            var removeTablesSql = @"
                                  DELETE ti
                                  FROM TableItems ti
                                  WHERE ti.UserId = @UserId";

            if (deleteBySensor)
            {
                removeTablesSql += " AND ti.SensorName = @SensorName";
                await connection.ExecuteAsync(removeTablesSql, new { UserId = userId, SensorName = sensorName }, transaction);
            }

            else await connection.ExecuteAsync(removeTablesSql, new { UserId = userId }, transaction);

            var removeBinaryGraphsSql = @"
                                        DELETE bgi
                                        FROM BinaryGraphItems bgi
                                        WHERE bgi.UserId = @UserId";

            if (deleteBySensor)
            {
                removeBinaryGraphsSql += " AND bgi.SensorName = @SensorName";
                await connection.ExecuteAsync(removeBinaryGraphsSql, new { UserId = userId, SensorName = sensorName }, transaction);
            }

            else await connection.ExecuteAsync(removeBinaryGraphsSql, new { UserId = userId }, transaction);
        }
    }
}
