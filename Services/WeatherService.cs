using System.Text.Json;
using WeatherStation.Models;

namespace WeatherStation.Services
{
    public class WeatherService
    {
        // Used to talk to websites
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<WeatherResponse?> GetWeatherAsync(double latitude, double longitude)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m,weather_code&daily=temperature_2m_max,temperature_2m_min,precipitation_probability_max,weather_code&timezone=auto";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // The website said no (e.g. bad address)
                if (!response.IsSuccessStatusCode)
                    return null;

                // Read the JSON text, then turn it into our classes
                string json = await response.Content.ReadAsStringAsync();
                WeatherResponse? weather = JsonSerializer.Deserialize<WeatherResponse>(json);
                return weather;
            }
            catch (Exception)
            {
                // Something went wrong (e.g. no internet)
                return null;
            }
        }
    }
}
