using System.Text.Json.Serialization;

namespace WeatherStation.Models
{
    // The full reply from the weather website
    public class WeatherResponse
    {
        [JsonPropertyName("current")]
        public CurrentWeather? Current { get; set; }
    }
}
