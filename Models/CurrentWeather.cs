using System.Text.Json.Serialization;

namespace WeatherStation.Models
{
    // The weather right now
    public class CurrentWeather
    {
        // The name in [ ] is the name used in the JSON
        [JsonPropertyName("temperature_2m")]
        public double? Temperature { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double? WindSpeed { get; set; }
    }
}
