using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;

namespace SurvivalBox.ViewModels
{
    public class MainWeatherViewModel : ActivityIndicatorViewModelBase
    {
        private IPageDialogService _dialogService;

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

        public MainWeatherViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;

            UseMetricUnits = true;
            UnitsToggledCommand = new DelegateCommand(OnUnitsToggled);

            RefreshWeather(true);
        }

        public async Task LoadWeather(bool showActivityIndicator)
        {
            using (new ActivityIndicatorScope(this, showActivityIndicator))
            {
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
            }
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
            RefreshWeather(true);
        }

        private async void RefreshWeather(bool showActivityIndicator)
        {
            try
            {
                await LoadWeather(showActivityIndicator);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e);
                await _dialogService.DisplayAlertAsync("Refresh Error",
                    "Make sure you have a working network connection!", "OK");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                await _dialogService.DisplayAlertAsync("Refresh Error", "Couldn't refresh data (" + e.Message + ")",
                    "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        protected override void OnRefresh()
        {
            RefreshWeather(false);
        }
    }
}
