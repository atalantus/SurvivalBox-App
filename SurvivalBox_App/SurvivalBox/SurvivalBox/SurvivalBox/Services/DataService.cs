using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SurvivalBox.Services
{
    public static class DataService
    {
        public static async Task<T> GetJSONData<T>(string queryString)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(queryString);

            var data = default(T);
            if (response != null)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(json);
            }

            return data;
        }
    }
}