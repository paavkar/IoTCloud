using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Models
{
    public class BinaryReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int Binary { get; set; }
        public ReadingType ReadingType { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset TimeOfMeasurement { get; set; }
        public string SensorName { get; set; }
    }
}
