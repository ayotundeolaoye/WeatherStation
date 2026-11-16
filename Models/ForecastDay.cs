namespace WeatherStation.Models
{
    // One row in the forecast list (one day)
    public class ForecastDay
    {
        public string Date { get; set; } = "";
        public string High { get; set; } = "";
        public string Low { get; set; } = "";
        public string Rain { get; set; } = "";
    }
}
