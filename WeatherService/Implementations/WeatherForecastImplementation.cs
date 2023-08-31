using static System.Net.WebRequestMethods;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherService.Implementations
{
    public class WeatherForecastImplementation
    {
        internal static WeatherForecast GetWeatherForecast(string postalCode)
        {
            //todo: retreive current temperature from:
            //https://www.weatherbit.io/api/swaggerui/weather-api-v2#!/Current32Weather32Data/get_current_postal_code_postal_code
            //using API Key: da024557070548d8af52af6b7a5ac763

            WeatherForecast forecast = new WeatherForecast();

            string apiKey = "da024557070548d8af52af6b7a5ac763";
            string getCurrentWeatherApi = $"https://api.weatherbit.io/v2.0/current?key={apiKey}&postal_code={postalCode}";

            var weatherBitResponse = CallWeatherApi(getCurrentWeatherApi).Result;

            forecast.Date = weatherBitResponse.Ob_Time;

            forecast.Temperature = new Temperature
            {
                Celsius = weatherBitResponse.App_temp,
                Fahrenheit = ConvertCelsiusToFahrenheit(weatherBitResponse.App_temp)
            };

            return forecast;
        }

        private static async Task<WeatherBitResponse> CallWeatherApi(string getCurrentWeatherApi)
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(getCurrentWeatherApi);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JObject.Parse(responseBody)["data"].First().ToObject<WeatherBitResponse>();
                }
                else
                {
                    throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
                }
            }
        }

       private static double ConvertCelsiusToFahrenheit(double celsius)
       {
            return Math.Round(((celsius * 9 / 5) + 32), 1);
       }
    }
}
