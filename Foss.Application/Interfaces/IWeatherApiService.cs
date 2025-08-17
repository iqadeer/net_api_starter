using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAPI.Application.Interfaces
{
    public interface IWeatherApiService
    {
        string[] GetWeather();
        void AddWeather(string newWeather);

        void DeleteWeather(string weatherToDelete);
        void UpdateWeather(string oldWeather, string newWeather);
        string GetWeatherByIndex(int index);
    }
}
