using System;
using System.Globalization;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SurvivalBox.Models
{
    public class Session
    {
        public enum States
        {
            ACTIVE,
            STOPPED,
            ENDED
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Done { get; set; }

        [JsonProperty(PropertyName = "state")]
        public States CurState { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public TimeSpan ActiveDuration { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime? EndDate { get; set; }

        [Version]
        public string Version { get; set; }

        public string StartDateString => GetStartDate();

        public Session(string name)
        {
            Name = name;
            Id = null;
            Done = false;
            ActiveDuration = new TimeSpan(0);
            EndDate = null;
        }

        public string GetStartDate()
        {
            // TODO: Check for settings
            return StartDate.ToLocalTime().ToString(CultureInfo.CurrentCulture);
        }
    }
}