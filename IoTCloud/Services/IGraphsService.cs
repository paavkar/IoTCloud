using IoTCloud.Models;

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
    }
}
