﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class WeatherManager
    {
        private static WeatherManager _instance;
        public static WeatherManager Instance => _instance ?? (_instance = new WeatherManager());

        private WeatherManager() { }

        public async Task<Weather> GetWeather(bool imperialUnits)
        {
            // TODO: Make location selectable
            Debug.WriteLine("Imperial: " + imperialUnits);
            var queryString =
                $"http://api.openweathermap.org/data/2.5/weather?lat=48.064220&lon=11.680739&APPID={Constants.OpenWeatherMapKey}&units={(imperialUnits ? "imperial" : "metric")}";
            var weather = await DataService.GetJSONData<Weather>(queryString).ConfigureAwait(false);

            return weather;
        }
    }
}