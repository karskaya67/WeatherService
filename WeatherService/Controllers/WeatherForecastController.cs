using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        private static readonly Dictionary<int, string> Summaries = new Dictionary<int, string>(){{0,
            "Freezing" }, {4,"Bracing" }, {10,"Chilly" } , {16,"Cool" }, {21,"Mild" }, {27,"Warm" }, {32,"Hot" }, {38,"Sweltering" }, {43,"Scorching" }
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetForecast")]
        public WeatherForecast Get([FromQuery] string postalCode)
        {
            WeatherForecast forecast = WeatherService.Implementations.WeatherForecastImplementation.GetWeatherForecast(postalCode);

            forecast.Summary = GetWeatherSummary(forecast.Temperature.Celsius);

            return forecast;
        }

        static string GetWeatherSummary(double temperature)
        {
            int closestKey = 0;

            foreach (var key in Summaries.Keys)
            {
                if (Math.Abs(key - temperature) < Math.Abs(closestKey - temperature))
                {
                    closestKey = key;
                }
            }

            return Summaries[closestKey];
        }
    }
}