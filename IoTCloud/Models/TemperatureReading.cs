using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTCloud.Models
{
    public class TemperatureReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public float Temperature { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset TimeOfMeasurement { get; set; }
        public string SensorName { get; set; }
    }
}
