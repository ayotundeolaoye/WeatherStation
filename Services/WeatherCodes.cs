namespace WeatherStation.Services
{
    // Turns Open-Meteo's weather code number into words and an icon.
    // The codes come from the table on open-meteo.com/en/docs
    public static class WeatherCodes
    {
        public static string GetDescription(int? code)
        {
            if (code == 0 || code == 1) return "Clear sky";
            if (code == 2) return "Partly cloudy";
            if (code == 3) return "Cloudy";
            if (code == 45 || code == 48) return "Fog";
            if (code >= 51 && code <= 67) return "Rain";
            if (code >= 71 && code <= 77) return "Snow";
            if (code == 80 || code == 81 || code == 82) return "Rain showers";
            if (code == 85 || code == 86) return "Snow showers";
            if (code >= 95) return "Thunderstorm";

            // Code is missing or one we don't know
            return "Unknown";
        }

        public static string GetIcon(int? code)
        {
            if (code == 0 || code == 1) return "☀️";
            if (code == 2) return "⛅";
            if (code == 3) return "☁️";
            if (code == 45 || code == 48) return "🌫️";
            if (code >= 51 && code <= 67) return "🌧️";
            if (code >= 71 && code <= 77) return "❄️";
            if (code == 80 || code == 81 || code == 82) return "🌦️";
            if (code == 85 || code == 86) return "❄️";
            if (code >= 95) return "⛈️";

            return "❔";
        }
    }
}
