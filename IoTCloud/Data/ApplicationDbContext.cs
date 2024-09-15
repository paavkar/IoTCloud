using IoTCloud.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IoTCloud.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<DistanceReading> DistanceReadings { get; set; }
        public DbSet<LuminosityReading> LuminosityReadings { get; set; }
        public DbSet<TemperatureReading> TemperatureReadings { get; set; }
        public DbSet<VelocityReading> VelocityReadings { get; set; }
        public DbSet<GraphItem> GraphItems { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
    }
}
