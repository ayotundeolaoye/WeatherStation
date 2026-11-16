namespace WeatherStation.Models
{
    // The forecast for the next few days.
    // Each list has one value per day, in the same order
    public class DailyWeather
    {
        public List<string>? time { get; set; }
        public List<double?>? temperature_2m_max { get; set; }
        public List<double?>? temperature_2m_min { get; set; }
        public List<double?>? precipitation_probability_max { get; set; }
        public List<int?>? weather_code { get; set; }
    }
}
