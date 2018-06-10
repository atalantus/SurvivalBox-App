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
            var weather = await WeatherManager.GetWeather();

            Debug.WriteLine($"Location: {weather.Location}");
            Debug.WriteLine($"Temperature: {weather.Main.Temperature} F");
            Debug.WriteLine($"WindData Speed: {weather.Wind.Speed} mph");
            Debug.WriteLine($"Humidity: {weather.Main.Humidity} %");
            Debug.WriteLine($"Visibility: {weather.Visibility}");
            Debug.WriteLine($"Time of Sunrise: {weather.Sunrise} UTC");
            Debug.WriteLine($"Time of Sunset: {weather.Sunset} UTC");

            WeatherProperties = new ObservableCollection<WeatherProperty>()
            {
                new WeatherProperty("Location", weather.Location),
                new WeatherProperty("Temperature", weather.Main.Temperature + " F"),
                new WeatherProperty("WindData Speed", weather.Wind.Speed + " mph"),
                new WeatherProperty("Humidity", weather.Main.Humidity + " %"),
                new WeatherProperty("Visibility", weather.Visibility),
                new WeatherProperty("Time of Sunrise", weather.Sunrise + "UTC"),
                new WeatherProperty("Time of Sunset", weather.Sunset + "UTC")
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
