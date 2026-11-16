namespace WeatherStation.Models
{
    // The full reply from the weather website
    public class WeatherResponse
    {
        public CurrentWeather? current { get; set; }
        public DailyWeather? daily { get; set; }
    }
}
