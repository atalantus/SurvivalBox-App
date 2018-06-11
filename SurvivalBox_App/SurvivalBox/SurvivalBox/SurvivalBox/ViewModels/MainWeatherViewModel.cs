using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using SurvivalBox.Models;
using SurvivalBox.Services;

namespace SurvivalBox.ViewModels
{
    public class MainWeatherViewModel : ActivityIndicatorViewModelBase
    {
        private ObservableCollection<WeatherProperty> _weatherProperties;
        public ObservableCollection<WeatherProperty> WeatherProperties
        {
            get => _weatherProperties;
            set => SetProperty(ref _weatherProperties, value);
        }

        public DelegateCommand UnitsToggledCommand { get; set; }

        private bool _useMetricUnits;
        public bool UseMetricUnits
        {
            get => _useMetricUnits;
            set => SetProperty(ref _useMetricUnits, value);
        }

        public MainWeatherViewModel()
        {
            UseMetricUnits = true;
            UnitsToggledCommand = new DelegateCommand(OnUnitsToggled);

            using (new ActivityIndicatorScope(this, true))
            {
                LoadWeather();
            }
        }

        private async void LoadWeather()
        {
            WeatherManager.Instance.UseImperialUnits = true;
            var weather = await WeatherManager.Instance.GetWeather(!UseMetricUnits);

            WeatherProperties = new ObservableCollection<WeatherProperty>()
            {
                new WeatherProperty("Time of Data", weather.DataTime),
                new WeatherProperty("Location", $"{weather.City}, {weather.SystemData.CountryCode}\nLon: {weather.LocationData.Longitude}\nLat: {weather.LocationData.Latitude}"),
                new WeatherProperty("Weather", $"{weather.WeatherData[0].Title}, {weather.WeatherData[0].Description}"),
                new WeatherProperty("Temperature", $"Current: {weather.MainData.Temperature} {(UseMetricUnits ? "°C" : "F")}\nMin: {weather.MainData.MinTemperature} {(UseMetricUnits ? "°C" : "F")}\nMax: {weather.MainData.MaxTemperature} {(UseMetricUnits ? "°C" : "F")}"),
                new WeatherProperty("Humidity", $"{weather.MainData.Humidity} %"),
                new WeatherProperty("Wind", $"Speed: {weather.WindData.Speed} {(UseMetricUnits ? "mps" : "mph")}\nDirection: {weather.WindData.GetWindDirection()}"),
                new WeatherProperty("Cloudiness", $"{weather.CloudData.Cloudiness} %"),
                new WeatherProperty("Sun", $"Sunrise: {weather.SystemData.SunriseTime}\nSunset: {weather.SystemData.SunsetTime}")
            };

            IsRefreshing = false;
        }

        public struct WeatherProperty
        {
            public string Title { get; set; }
            public string Content { get; set; }

            public WeatherProperty(string title, string content)
            {
                Title = title;
                Content = content;
            }
        }

        private void OnUnitsToggled()
        {
            using (new ActivityIndicatorScope(this, true))
            {
                LoadWeather();
            }
        }

        protected override void OnRefresh()
        {
            using (new ActivityIndicatorScope(this, false))
            {
                LoadWeather();
            }
        }
    }
}
