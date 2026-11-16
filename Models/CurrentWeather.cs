namespace WeatherStation.Models
{
    // The weather right now. Names match the JSON exactly
    public class CurrentWeather
    {
        public double? temperature_2m { get; set; }
        public double? wind_speed_10m { get; set; }
    }
}
