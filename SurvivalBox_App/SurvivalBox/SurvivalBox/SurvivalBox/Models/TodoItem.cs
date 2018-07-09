using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SurvivalBox.Models
{
    public class TodoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Done { get; set; }

        [Version]
        public string Version { get; set; }
    }
}