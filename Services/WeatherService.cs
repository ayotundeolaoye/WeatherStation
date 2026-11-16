using System.Text.Json;
using WeatherStation.Models;

namespace WeatherStation.Services
{
    public class WeatherService
    {
        // Used to talk to websites
        private static readonly HttpClient _httpClient = new HttpClient();

        // Gets the weather for a place: from the saved file if it's new enough,
        // otherwise from the internet
        public async Task<CachedWeather?> GetWeatherAsync(Place place)
        {
            // Each place gets its own save file, e.g. weather_Galway.json
            string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "weather_" + place.Name + ".json");

            CachedWeather? saved = LoadFromFile(fileName);

            // Use the saved weather if it's less than 30 minutes old
            if (saved != null && (DateTime.Now - saved.SavedAt).TotalMinutes < 30)
                return saved;

            WeatherResponse? weather = await DownloadWeatherAsync(place.Latitude, place.Longitude);

            // Download failed, so give back the old saved weather (null if there is none)
            if (weather == null)
                return saved;

            // Download worked, so save it with the time
            CachedWeather fresh = new CachedWeather();
            fresh.SavedAt = DateTime.Now;
            fresh.Weather = weather;
            SaveToFile(fileName, fresh);

            return fresh;
        }

        private async Task<WeatherResponse?> DownloadWeatherAsync(double latitude, double longitude)
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

        private CachedWeather? LoadFromFile(string fileName)
        {
            // First time running: there's no file yet
            if (!File.Exists(fileName))
                return null;

            try
            {
                string json = File.ReadAllText(fileName);
                CachedWeather? saved = JsonSerializer.Deserialize<CachedWeather>(json);
                return saved;
            }
            catch (Exception)
            {
                // The file is damaged, so ignore it
                return null;
            }
        }

        private void SaveToFile(string fileName, CachedWeather data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data);
                File.WriteAllText(fileName, json);
            }
            catch (Exception)
            {
                // Couldn't save, but the app can still carry on
            }
        }
    }
}
