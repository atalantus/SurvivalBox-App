using System.Threading.Tasks;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class WeatherManager
    {
        public static async Task<Weather> GetWeather()
        {
            // TODO: Make location selectable
            var queryString = "http://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=" + Constants.OpenWeatherMapKey + "&units=imperial";

            var weather = await DataService.GetJSONData<Weather>(queryString).ConfigureAwait(false);

            return weather;
        }
    }
}