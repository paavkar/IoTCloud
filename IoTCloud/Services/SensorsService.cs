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
                sensor.CreatedAt = DateTimeOffset.Now;
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

        public async Task<bool> EditSensor(Sensor sensor, string oldSensorName)
        {
            var dbSensor = await context.Sensors.FindAsync(sensor.Id);

            if (dbSensor is not null)
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var updateSensorSql = @"
                                                  UPDATE Sensors
                                                  SET Name = @SensorName, ModifiedAt = @ModifiedAt
                                                  WHERE Id = @Id";

                            var sensorUpdated = await connection.ExecuteAsync(updateSensorSql, new { sensor.Id, SensorName = sensor.Name, ModifiedAt = DateTimeOffset.Now }, transaction);

                            var updateDistanceReadingSql = @"
                                                           UPDATE DistanceReadings
                                                           SET SensorName = @SensorName
                                                           WHERE UserId = @UserId
                                                           AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateDistanceReadingSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateHumidityReadingSql = @"
                                                           UPDATE HumidityReadings
                                                           SET SensorName = @SensorName
                                                           WHERE UserId = @UserId
                                                           AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateHumidityReadingSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateLuminosityReadingSql = @"
                                                             UPDATE LuminosityReadings
                                                             SET SensorName = @SensorName
                                                             WHERE UserId = @UserId
                                                             AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateLuminosityReadingSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateTemperatureReadingSql = @"
                                                              UPDATE TemperatureReadings
                                                              SET SensorName = @SensorName
                                                              WHERE UserId = @UserId
                                                              AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateTemperatureReadingSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateVelocityReadingSql = @"
                                                           UPDATE VelocityReadings
                                                           SET SensorName = @SensorName
                                                           WHERE UserId = @UserId
                                                           AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateVelocityReadingSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateBinaryReadingSql = @"
                                                         UPDATE BinaryReadings
                                                         SET SensorName = @SensorName
                                                         WHERE UserId = @UserId
                                                         AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateBinaryReadingSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateGraphItemsSql = @"
                                                      UPDATE GraphItems
                                                      SET SensorName = @SensorName
                                                      WHERE UserId = @UserId
                                                      AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateGraphItemsSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateTablesSql = @"
                                                  UPDATE TableItems
                                                  SET SensorName = @SensorName
                                                  WHERE UserId = @UserId
                                                  AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateTablesSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);

                            var updateBinaryGraphsSql = @"
                                                        UPDATE BinaryGraphItems
                                                        SET SensorName = @SensorName
                                                        WHERE UserId = @UserId
                                                        AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateBinaryGraphsSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);


                            var updateEmailNotificationsSql = @"
                                                              UPDATE EmailNotifications
                                                              SET SensorName = @SensorName
                                                              WHERE UserId = @UserId
                                                              AND SensorName = @OldSensorName";

                            await connection.ExecuteAsync(updateEmailNotificationsSql, new { sensor.UserId, SensorName = sensor.Name, OldSensorName = oldSensorName }, transaction);


                            transaction.Commit();

                            return sensorUpdated > 0;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            return false;
        }
    }
}
