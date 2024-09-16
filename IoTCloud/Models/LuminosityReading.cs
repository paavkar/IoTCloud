using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTCloud.Models
{
    public class LuminosityReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public float Luminosity { get; set; }
        public string UserId { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public string SensorName { get; set; }
    }
}
