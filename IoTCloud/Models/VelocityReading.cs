using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTCloud.Models
{
    public class VelocityReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public float Velocity { get; set; }
        public string UserId { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
    }
}
