using IoTCloud.Models;
using Microsoft.Data.SqlClient;

namespace IoTCloud.Services
{
    public interface IGraphsService
    {
        Task<bool> AddUserGraph(GraphItem item);
        Task<List<GraphItem>> GetUserGraphs(string userId);
        Task<bool> DeleteUserGraphs(string userId);
        Task<bool> DeleteGraph(string id);

        Task<bool> AddUserTable(TableItem item);
        Task<List<TableItem>> GetUserTables(string userId);
        Task<bool> DeleteUserTables(string userId);
        Task<bool> DeleteTable(string id);

        Task<bool> AddUserBinaryGraph(BinaryGraphItem item);
        Task<List<BinaryGraphItem>> GetUserBinaryGraphItems(string userId);
        Task<bool> DeleteUserBinaryGraphs(string userId);
        Task<bool> DeleteBinaryGraph(string id);

        Task DeleteGraphsBySensor(string sensorName, string userId, SqlConnection connection, SqlTransaction transaction, bool deleteBySensor = false);
    }
}
