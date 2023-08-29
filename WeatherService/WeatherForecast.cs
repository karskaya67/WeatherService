using System.Text.Json.Serialization;

namespace WeatherService
{
    public class Temperature
    {
        public double Celsius { get; set; }

        public double Fahrenheit { get; set; }
    }


    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public Temperature Temperature { get; set; }

        public string Summary { get; set; }
    }
}