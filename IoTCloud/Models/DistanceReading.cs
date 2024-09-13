using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTCloud.Models
{
    public class DistanceReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public float Distance { get; set; }
        public string UserId { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
    }
}
