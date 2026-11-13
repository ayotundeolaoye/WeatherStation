using WeatherStation.Models;
using WeatherStation.Services;

namespace WeatherStation
{
    public partial class MainPage : ContentPage
    {
        WeatherService weatherService = new WeatherService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGetWeatherClicked(object? sender, EventArgs e)
        {
            TemperatureLabel.Text = "Loading...";
            WindLabel.Text = "";

            // 53.27, -9.05 is Galway on the map
            WeatherResponse? weather = await weatherService.GetWeatherAsync(53.27, -9.05);

            // Show a message if we got nothing back
            if (weather == null || weather.Current == null)
            {
                TemperatureLabel.Text = "Couldn't get the weather. Check your internet.";
                return;
            }

            TemperatureLabel.Text = weather.Current.Temperature + " °C";
            WindLabel.Text = "Wind: " + weather.Current.WindSpeed + " km/h";
        }
    }
}
