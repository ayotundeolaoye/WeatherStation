using WeatherStation.Models;
using WeatherStation.Services;

namespace WeatherStation
{
    public partial class MainPage : ContentPage
    {
        private WeatherService _weatherService = new WeatherService();
        private List<Place> _places = new List<Place>();

        public MainPage()
        {
            InitializeComponent();
            // Added the places and their position
            _places.Add(new Place { Name = "Galway", Latitude = 53.27, Longitude = -9.05 });
            _places.Add(new Place { Name = "Dublin", Latitude = 53.35, Longitude = -6.26 });
            _places.Add(new Place { Name = "Athlone", Latitude = 53.42, Longitude = -7.94 });
            _places.Add(new Place { Name = "Cork", Latitude = 51.90, Longitude = -8.47 });
            _places.Add(new Place { Name = "London", Latitude = 51.51, Longitude = -0.13 });

            // Put each place's name into the dropdown
            foreach (Place place in _places)
            {
                PlacePicker.Items.Add(place.Name);
            }

            PlacePicker.SelectedIndex = 0;
        }

        private async void OnGetWeatherClicked(object? sender, EventArgs e)
        {
            // -1 means nothing is picked
            if (PlacePicker.SelectedIndex == -1)
            {
                TemperatureLabel.Text = "Please choose a place first.";
                return;
            }

            // The picked name is at the same position in the list
            Place selectedPlace = _places[PlacePicker.SelectedIndex];

            TemperatureLabel.Text = "Loading...";
            WindLabel.Text = "";
            ForecastLabel.Text = "";

            WeatherResponse? weather = await _weatherService.GetWeatherAsync(selectedPlace.Latitude, selectedPlace.Longitude);

            // Show a message if we got nothing back
            if (weather == null || weather.current == null)
            {
                TemperatureLabel.Text = "Couldn't get the weather. Check your internet.";
                return;
            }

            TemperatureLabel.Text = selectedPlace.Name + ": " + weather.current.temperature_2m + " °C";
            WindLabel.Text = "Wind: " + weather.current.wind_speed_10m + " km/h";

            // Show a message if the forecast part is missing
            if (weather.daily == null
                || weather.daily.time == null
                || weather.daily.temperature_2m_max == null
                || weather.daily.temperature_2m_min == null
                || weather.daily.precipitation_probability_max == null)
            {
                ForecastLabel.Text = "No forecast available.";
                return;
            }

            // Build one line of text for each day
            string forecastText = "";

            for (int i = 0; i < weather.daily.time.Count; i++)
            {
                forecastText = forecastText
                    + weather.daily.time[i]
                    + "   High " + weather.daily.temperature_2m_max[i] + " °C"
                    + "   Low " + weather.daily.temperature_2m_min[i] + " °C"
                    + "   Rain " + weather.daily.precipitation_probability_max[i] + "%"
                    + "\n";
            }

            ForecastLabel.Text = forecastText;
        }
    }
}
