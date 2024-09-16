using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static IoTCloud.Models.Enums;

namespace IoTCloud.Models
{
    public class EmailNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; }
        [Required]
        public string NotificationMessage { get; set; } = string.Empty;
        [Required]
        public Threshold NotificationThreshold { get; set; }
        [Required]
        public ReadingType ReadingType { get; set; }
        [Required]
        public float ThresholdValue { get; set; } = 0;
        public bool IsBinary { get; set; } = false;
        public string SensorName { get; set; }
    }
}
