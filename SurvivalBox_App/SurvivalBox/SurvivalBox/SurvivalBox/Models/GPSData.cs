using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

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

        [JsonIgnore]
        public Position Position => new Position(Latitude, Longitude);

        [JsonIgnore]
        public string Label => CreationDate.ToLocalTime().ToString("g") + " (UTC)";

        [JsonIgnore] public string Address => $"Velocity: {Velocity:##.0}";

        public GPSData(double latitude, double longitude, double velocity, DateTime creationDate)
        {
            CreationDate = creationDate;
            Longitude = longitude;
            Latitude = latitude;
            Velocity = velocity;
        }

        public GPSData() { }
    }
}