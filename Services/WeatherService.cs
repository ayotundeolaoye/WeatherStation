using System.Net.Http.Json;
using WeatherStation.Models;

namespace WeatherStation.Services
{
    public class WeatherService
    {
        // Used to talk to websites
        HttpClient client = new HttpClient();

        public async Task<WeatherResponse?> GetWeatherAsync(double latitude, double longitude)
        {
            // The web address we ask for the weather
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m";

            try
            {
                // Get the JSON and turn it into our classes
                WeatherResponse? weather = await client.GetFromJsonAsync<WeatherResponse>(url);
                return weather;
            }
            catch (Exception)
            {
                // if Something went wrong
                return null;
            }
        }
    }
}
