namespace WeatherStation.Models
{
    // The weather we saved to a file, and when we got it
    public class CachedWeather
    {
        public DateTime SavedAt { get; set; }
        public WeatherResponse? Weather { get; set; }
    }
}
