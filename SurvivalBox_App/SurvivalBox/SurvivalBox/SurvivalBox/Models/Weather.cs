using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace SurvivalBox.Models
{
    public class Weather
    {
        #region Fields

        [JsonProperty("coord")]
        public LocationData LocationData { get; set; }

        [JsonProperty("weather")]
        public List<WeatherData> WeatherData { get; set; }

        [JsonProperty("main")]
        public MainData MainData { get; set; }

        [JsonProperty("wind")]
        public WindData WindData { get; set; }

        [JsonProperty("clouds")]
        public CloudData CloudData { get; set; }

        [JsonProperty("rain")]
        public RainData RainData { get; set; }

        [JsonProperty("snow")]
        public SnowData SnowData { get; set; }

        private string _dataTime;
        /// <summary>
        /// Time of data calculation
        /// </summary>
        [JsonProperty("dt")]
        public string DataTime { get => _dataTime; set => CalculateDataTime(ref _dataTime, value); }

        [JsonProperty("sys")]
        public SystemData SystemData { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        [JsonProperty("name")]
        public string City { get; set; }

        #endregion

        public void CalculateDataTime(ref string data, string unixTime)
        {
            var dataTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            data = dataTime.AddSeconds(double.Parse(unixTime)).ToString(CultureInfo.CurrentCulture);
        }
    }

    public struct MainData
    {
        /// <summary>
        /// Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("temp")]
        public string Temperature { get; set; }
        /// <summary>
        /// Atmospheric pressure (on the sea level, if there is no sea_level or grnd_level data), hPa
        /// </summary>
        [JsonProperty("Pressure")]
        public string Pressure { get; set; }
        /// <summary>
        /// Humidity, %
        /// </summary>
        [JsonProperty("humidity")]
        public string Humidity { get; set; }
        /// <summary>
        /// Minimum temperature at the moment. This is deviation from current temp that is possible for large cities
        /// and megalopolises geographically expanded (use these parameter optionally).
        /// Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("temp_min")]
        public string MinTemperature { get; set; }
        /// <summary>
        /// Maximum temperature at the moment. This is deviation from current temp that is possible for large cities
        /// and megalopolises geographically expanded (use these parameter optionally).
        /// Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("temp_max")]
        public string MaxTemperature { get; set; }
        /// <summary>
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        [JsonProperty("sea_level")]
        public string SeaLevelPressure { get; set; }
        /// <summary>
        /// Atmospheric pressure on the ground level, hPa
        /// </summary>
        [JsonProperty("grnd_level")]
        public string GroundLevelPressure { get; set; }
    }

    public struct WindData
    {
        /// <summary>
        /// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
        /// </summary>
        [JsonProperty("speed")]
        public string Speed { get; set; }
        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("deg")]
        public string Degrees { get; set; }

        public enum CardinalDirections
        {
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest
        }

        public CardinalDirections GetWindDirection()
        {
            var degrees = double.Parse(Degrees);

            if (degrees <= 33.75)
                return CardinalDirections.North;
            if (degrees <= 78.75)
                return CardinalDirections.NorthEast;
            if (degrees <= 123.75)
                return CardinalDirections.East;
            if (degrees <= 168.75)
                return CardinalDirections.SouthEast;
            if (degrees <= 213.75)
                return CardinalDirections.South;
            if (degrees <= 258.75)
                return CardinalDirections.SouthWest;
            if (degrees <= 303.75)
                return CardinalDirections.West;
            if (degrees <= 348.75)
                return CardinalDirections.NorthWest;

            return CardinalDirections.North;
        }
    }

    public struct CloudData
    {
        /// <summary>
        /// Cloudiness, %
        /// </summary>
        [JsonProperty("all")]
        public string Cloudiness { get; set; }
    }

    public struct RainData
    {
        /// <summary>
        /// Rain volume for the last 3 hours
        /// </summary>
        [JsonProperty("3h")]
        public string Volume { get; set; }
    }

    public struct SnowData
    {
        /// <summary>
        /// Snow volume for the last 3 hours
        /// </summary>
        [JsonProperty("3h")]
        public string Volume { get; set; }
    }

    public struct SystemData
    {
        /// <summary>
        /// Country code (GB, JP etc.)
        /// </summary>
        [JsonProperty("country")]
        public string CountryCode { get; set; }
        /// <summary>
        /// Sunrise time
        /// </summary>
        [JsonProperty("sunrise")]
        public string SunriseTime { get => _sunriseTime; set => CalculateSunStateTime(ref _sunriseTime, value); }
        /// <summary>
        /// Sunset time
        /// </summary>
        [JsonProperty("sunset")]
        public string SunsetTime { get => _sunsetTime; set => CalculateSunStateTime(ref _sunsetTime, value); }

        private string _sunriseTime;
        private string _sunsetTime;

        private void CalculateSunStateTime(ref string sunState, string unixTime)
        {
            var sunStateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            sunState = sunStateTime.AddSeconds(double.Parse(unixTime)).ToString(CultureInfo.CurrentCulture);
        }
    }

    public struct WeatherData
    {
        /// <summary>
        /// Group of weather parameters (Rain, Snow, Extreme etc.)
        /// </summary>
        [JsonProperty("main")]
        public string Title { get; set; }
        /// <summary>
        /// Weather condition within the group
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public struct LocationData
    {
        /// <summary>
        /// City geo location, longitude
        /// </summary>
        [JsonProperty("lon")]
        public string Longitude { get; set; }
        /// <summary>
        /// City geo location, latitude
        /// </summary>
        [JsonProperty("lat")]
        public string Latitude { get; set; }
    }
}