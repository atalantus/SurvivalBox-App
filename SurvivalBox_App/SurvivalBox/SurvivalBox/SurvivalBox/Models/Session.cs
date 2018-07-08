using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SurvivalBox.Services;
using Xamarin.Forms;

namespace SurvivalBox.Models
{
    public delegate void UpdatedTimerEventHandler(Session sender, string time);

    public class Session
    {
        public enum States
        {
            ACTIVE,
            ENDED
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Done { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }

        [JsonProperty(PropertyName = "state")]
        public States CurState { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime? EndDate { get; set; }

        [Version]
        public string Version { get; set; }

        public string StartDateString => GetStartDate();

        public string GetStartDate()
        {
            // TODO: Check for settings
            return StartDate.ToLocalTime().Date.ToString("d");
        }

        public bool CancelTimer { get; set; }
        public event UpdatedTimerEventHandler UpdatedTimer;
        private string _duration;

        public void StartSessionTimer()
        {
            CountSessionTime();
        }

        public string GetCurDuration()
        {
            return _duration;
        }

        private void CountSessionTime()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                if (!CancelTimer)
                {
                    // Debug.WriteLine($"Now: {DateTime.UtcNow:T} | Start: {StartDate.ToUniversalTime():T}");
                    var time = DateTime.UtcNow - StartDate.ToUniversalTime();
                    _duration = time.ToString(@"hh\:mm\:ss");
                    UpdatedTimer?.Invoke(this, _duration);
                    return true;
                }
                else
                {
                    CancelTimer = false;
                    return false;
                }
            });
        }
    }
}