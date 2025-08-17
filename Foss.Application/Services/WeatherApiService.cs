using NetAPI.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace NetAPI.Application.Services
{
    public class WeatherApiService(ILogger<WeatherApiService> logger, List<string> summaries) : IWeatherApiService
    {
        public string[] GetWeather()
        {
            logger.LogInformation("Getting weather");
            return summaries.ToArray();
        }

        public void AddWeather(string newWeather)
        {
            summaries.Add(newWeather);
        }

        public void DeleteWeather(string weatherToDelete)
        {
            summaries.Remove(weatherToDelete);
        }

        public void UpdateWeather(string oldWeather, string newWeather)
        {
            var index = summaries.IndexOf(oldWeather);
            if (index != -1)
            {
                summaries[index] = newWeather;
            }
        }

        public string GetWeatherByIndex(int index)
        {
            return summaries[index];
        }
    }
}
