namespace WeatherStation.Models
{
    // A place the user can pick, with its map position
    public class Place
    {
        public string Name { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
