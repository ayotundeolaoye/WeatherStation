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
            ConditionLabel.Text = "";
            WindLabel.Text = "";
            ForecastTitleLabel.Text = "";
            ForecastList.ItemsSource = null;

            WeatherResponse? weather = await _weatherService.GetWeatherAsync(selectedPlace.Latitude, selectedPlace.Longitude);

            // Show a message if we got nothing back
            if (weather == null || weather.current == null)
            {
                TemperatureLabel.Text = "Couldn't get the weather. Check your internet.";
                return;
            }

            TemperatureLabel.Text = selectedPlace.Name + ": " + weather.current.temperature_2m + " °C";
            WindLabel.Text = "Wind: " + weather.current.wind_speed_10m + " km/h";

            // Turn the weather code number into an icon and words
            int? code = weather.current.weather_code;
            ConditionLabel.Text = WeatherCodes.GetIcon(code) + " " + WeatherCodes.GetDescription(code);

            // Show a message if the forecast part is missing
            if (weather.daily == null
                || weather.daily.time == null
                || weather.daily.temperature_2m_max == null
                || weather.daily.temperature_2m_min == null
                || weather.daily.precipitation_probability_max == null
                || weather.daily.weather_code == null)
            {
                ForecastTitleLabel.Text = "No forecast available.";
                return;
            }

            // Make one ForecastDay (one row) for each day
            List<ForecastDay> days = new List<ForecastDay>();

            for (int i = 0; i < weather.daily.time.Count; i++)
            {
                ForecastDay day = new ForecastDay();
                day.Date = weather.daily.time[i];
                day.Icon = WeatherCodes.GetIcon(weather.daily.weather_code[i]);
                day.High = "High " + weather.daily.temperature_2m_max[i] + " °C";
                day.Low = "Low " + weather.daily.temperature_2m_min[i] + " °C";
                day.Rain = "Rain " + weather.daily.precipitation_probability_max[i] + "%";
                days.Add(day);
            }

            // Show the rows in the list on screen
            ForecastTitleLabel.Text = "Next 7 days";
            ForecastList.ItemsSource = days;
        }
    }
}
