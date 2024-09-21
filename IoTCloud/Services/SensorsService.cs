using Dapper;
using IoTCloud.Data;
using IoTCloud.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IoTCloud.Services
{
    public class SensorsService(ApplicationDbContext context, IConfiguration configuration, IGraphsService graphsService, IUserService userService) : ISensorsService
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
            try
            {
                var existingSensor = await context.Sensors.FirstOrDefaultAsync(s => s.Name == sensorName && s.UserId == userId);

                if (existingSensor != null) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSensor(string id, string sensorName, string userId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await userService.DeleteReadingsBySensor(sensorName, userId, connection, transaction, true);

                        await graphsService.DeleteGraphsBySensor(sensorName, userId, connection, transaction, true);

                        await userService.DeleteNotificationsBySensor(sensorName, userId, connection, transaction, true);

                        var sql = @"
                                  DELETE s
                                  FROM Sensors s
                                  WHERE s.Id = @Id";

                        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id }, transaction);

                        transaction.Commit();

                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<List<Sensor>> GetSensors(string userId)
        {
            var sensors = await context.Sensors.Where(s => s.UserId == userId).ToListAsync();

            return sensors;
        }
    }
}
