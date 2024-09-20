using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IoTCloud.Services
{
    public class SensorsService(ApplicationDbContext context, IConfiguration configuration) : ISensorsService
    {
        private SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddSensor(Sensor sensor)
        {
            var existingSensor = await context.Sensors.FirstOrDefaultAsync(s => s.Name == sensor.Name && s.UserId == sensor.UserId);

            if (existingSensor is null)
            {
                await context.Sensors.AddAsync(sensor);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CheckSensorExists(string sensorName, string userId)
        {
            var existingSensor = await context.Sensors.FirstOrDefaultAsync(s => s.Name == sensorName && s.UserId == userId);

            if (existingSensor != null) return true;
            return false;
        }

        public async Task<bool> DeleteSensor(string id, string sensorName)
        {
            using var connection = GetConnection();

            var sql = @"
                        DELETE s
                        FROM Sensors s
                        WHERE s.Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }

        public async Task<List<Sensor>> GetSensors(string userId)
        {
            var sensors = await context.Sensors.Where(s => s.UserId == userId).ToListAsync();

            return sensors;
        }
    }
}
