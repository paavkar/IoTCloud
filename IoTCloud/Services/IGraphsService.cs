using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface IGraphsService
    {
        Task<bool> AddUserGraph(GraphItem item, string userId);
        Task<List<GraphItem>> GetUserGraphs(string userId);
    }
}
