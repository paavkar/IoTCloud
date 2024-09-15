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
        public string Email { get; set; }
        public string UserId { get; set; }
        public string NotificationMessage { get; set; }
        public Threshold NotificationThreshold { get; set; }
        public ReadingType ReadingType { get; set; }
        public float ThresholdValue { get; set; }
    }
}
