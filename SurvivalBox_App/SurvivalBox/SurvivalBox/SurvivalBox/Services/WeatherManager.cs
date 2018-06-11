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

        public bool UseImperialUnits { get; set; }

        public async Task<Weather> GetWeather()
        {
            // TODO: Make location selectable
            var queryString = $"http://api.openweathermap.org/data/2.5/weather?lat=35&lon=139&APPID={Constants.OpenWeatherMapKey}&units={(UseImperialUnits ? "imperial" : "metric")}";
            var weather = await DataService.GetJSONData<Weather>(queryString).ConfigureAwait(false);

            return weather;
        }
    }
}