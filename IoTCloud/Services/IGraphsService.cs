using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface IGraphsService
    {
        Task<bool> AddUserGraph(GraphItem item);
        Task<List<GraphItem>> GetUserGraphs(string userId);
        Task<bool> DeleteUserGraphs(string userId);
        Task<bool> DeleteGraph(string id);
    }
}
