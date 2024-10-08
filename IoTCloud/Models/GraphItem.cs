﻿using ApexCharts;
using System.ComponentModel.DataAnnotations;

namespace IoTCloud.Models
{
    public record GraphItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string? DataType { get; set; }
        public string? Name { get; set; } = string.Empty;
        public SeriesType GraphType { get; set; }
        public string SensorName { get; set; }
    }
}
