using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.Logging;
using NetAPI.Application.Interfaces;
using NetAPI.Application.Services;
using Shouldly;

namespace NetAPI.Application.Tests.Services
{
    public class WeatherApiServiceTests
    {
        private readonly List<string> _summaries =
            ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

        [Fact]
        public void ShouldBeOfAssignableTo()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.ShouldBeAssignableTo<IWeatherApiService>();
        }

        [Fact]
        public void ShouldGetWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(10);
            weather.ShouldContain("Warm");
        }

        [Fact]
        public void ShouldAddWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.AddWeather("New_Weather");
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(11);
            weather.ShouldContain("New_Weather");
        }

        [Fact]
        public void ShouldDeleteWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.DeleteWeather("Cool");
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(9);
            weather.ShouldNotContain("Cool");
        }

        [Fact]
        public void ShouldUpdateWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.UpdateWeather("Cool", "Cooler");
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(10);
            weather.ShouldNotContain("Cool");
            weather.ShouldContain("Cooler");
        }

        [Theory]
        [InlineData(0, "zero")]
        [InlineData(1, "one")]
        [InlineData(3, "three")]
        [InlineData(9, "ten")]
        public void GetWeatherByIndex(int index, string value)
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var weatherList = _summaries;
            weatherList[index] = value;

            var sut = new WeatherApiService(mockedLogger, weatherList);
            var weathers = sut.GetWeather();
            weathers.Length.ShouldBe(10);
            var weather = sut.GetWeatherByIndex(index);
            weather.ShouldNotBeNull();
            weather.ShouldBe(value);
        }
    }

}
