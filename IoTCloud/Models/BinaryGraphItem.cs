using System.ComponentModel.DataAnnotations;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Models
{
    public class BinaryGraphItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; } = string.Empty;
        public string UserId { get; set; }
        public ReadingType ReadingType { get; set; }
        public string SensorName { get; set; }
    }
}
