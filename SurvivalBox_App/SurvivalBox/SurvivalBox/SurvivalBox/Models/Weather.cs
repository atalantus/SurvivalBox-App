using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurvivalBox.Models
{
    public class Weather
    {
        [JsonProperty("name")]
        public string Location { get; set; }

        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("wind")]
        public WindData Wind { get; set; }

        [JsonProperty("weather.0.main")]
        public string Visibility { get; set; }

        private string _sunrise = String.Empty;
        [JsonProperty("sys.sunrise")]
        public string Sunrise { get => _sunrise; set => SetSunTime(ref _sunrise, value); }

        private string _sunset = String.Empty;
        [JsonProperty("sys.sunset")]
        public string Sunset { get => _sunset; set => SetSunTime(ref _sunset, value); }

        private void SetSunTime(ref string sunState, string time)
        {
            var sunStateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            sunState = sunStateTime.AddSeconds(double.Parse(time)).ToString();
        }
    }

    public struct MainData
    {
        [JsonProperty("temp")]
        public string Temperature { get; set; }
        [JsonProperty("humidity")]
        public string Humidity { get; set; }
    }

    public struct WindData
    {
        [JsonProperty("speed")]
        public string Speed { get; set; }
    }

    public struct WeatherData
    {
        public List<List<string>> weatherData;
    }
}