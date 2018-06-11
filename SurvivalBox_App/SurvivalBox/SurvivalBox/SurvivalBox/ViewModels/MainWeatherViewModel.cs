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

        public MainWeatherViewModel()
        {
            using (new ActivityIndicatorScope(this, true))
            {
                LoadWeather();
            }
        }

        private async void LoadWeather()
        {
            WeatherManager.Instance.UseImperialUnits = true;
            var weather = await WeatherManager.Instance.GetWeather();

            Debug.WriteLine($"Location: {weather.City}");
            Debug.WriteLine($"Temperature: {weather.MainData.Temperature} F");
            Debug.WriteLine($"WindData Speed: {weather.WindData.Speed} mph");
            Debug.WriteLine($"Humidity: {weather.MainData.Humidity} %");
            Debug.WriteLine($"Visibility: {weather.WeatherDatas[0].Title}");
            Debug.WriteLine($"Time of Sunrise: {weather.SystemData.SunriseTime} UTC");
            Debug.WriteLine($"Time of Sunset: {weather.SystemData.SunsetTime} UTC");

            WeatherProperties = new ObservableCollection<WeatherProperty>()
            {
                new WeatherProperty("Location", weather.City),
                new WeatherProperty("Temperature", weather.MainData.Temperature + " F"),
                new WeatherProperty("WindData Speed", weather.WindData.Speed + " mph"),
                new WeatherProperty("Humidity", weather.MainData.Humidity + " %"),
                new WeatherProperty("Visibility", weather.WeatherDatas[0].Title),
                new WeatherProperty("Time of Sunrise", weather.SystemData.SunriseTime + " UTC"),
                new WeatherProperty("Time of Sunset", weather.SystemData.SunsetTime + " UTC")
            };
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

        protected override void OnRefresh()
        {
            using (new ActivityIndicatorScope(this, false))
            {
                LoadWeather();
            }
        }
    }
}
