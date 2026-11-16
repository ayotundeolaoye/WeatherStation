using WeatherStation.Models;
using WeatherStation.Services;

namespace WeatherStation
{
    public partial class MainPage : ContentPage
    {
        private WeatherService _weatherService = new WeatherService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGetWeatherClicked(object? sender, EventArgs e)
        {
            TemperatureLabel.Text = "Loading...";
            WindLabel.Text = "";

            // 53.27, -9.05 is Galway on the map
            WeatherResponse? weather = await _weatherService.GetWeatherAsync(53.27, -9.05);

            // Show a message if we got nothing back
            if (weather == null || weather.current == null)
            {
                TemperatureLabel.Text = "Couldn't get the weather. Check your internet.";
                return;
            }

            TemperatureLabel.Text = weather.current.temperature_2m + " °C";
            WindLabel.Text = "Wind: " + weather.current.wind_speed_10m + " km/h";
        }
    }
}
