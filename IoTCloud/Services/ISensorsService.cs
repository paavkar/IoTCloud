using IoTCloud.Models;

namespace IoTCloud.Services
{
    public interface ISensorsService
    {
        Task<bool> AddSensor(Sensor sensor);
        Task<List<Sensor>> GetSensors(string userId);
        Task<bool> DeleteSensor(string id, string sensorName);
        Task<bool> CheckSensorExists(string sensorName, string userId);
    }
}
