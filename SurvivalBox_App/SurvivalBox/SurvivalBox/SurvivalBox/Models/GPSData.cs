using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SurvivalBox.Models
{
    public class GPSData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "long")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "velocity")]
        public double Velocity { get; set; }

        [Version]
        public string Version { get; set; }
    }
}