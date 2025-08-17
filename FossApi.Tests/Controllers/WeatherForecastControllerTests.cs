using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetAPI.API.Controllers;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
using NetAPI.Application.Validations;
using NSubstitute;
using Shouldly;

namespace NetAPI.API.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherApiService _weatherApiService;
        private readonly WeatherForecastController _sut;
        private readonly IValidator<UpdateWeatherRequest> _uv;
        public WeatherForecastControllerTests()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _logger = fixture.Create<ILogger<WeatherForecastController>>();
            _weatherApiService = fixture.Create<IWeatherApiService>();
            _uv = fixture.Create<IValidator<UpdateWeatherRequest>>();
            _sut = new WeatherForecastController(_logger, _weatherApiService, _uv);
        }
        [Fact]
        public void WeatherForecastControllerBeOfType()
        {
            _sut.ShouldBeOfType<WeatherForecastController>();
        }

        [Theory]
        [InlineData("one", "two", "three")]

        public void ShouldReturnCorrectWeather(params string[] weatherList)
        {
            _weatherApiService.GetWeather().Returns(weatherList);
           

            var response = _sut.Get();
            response.ShouldBeOfType<ActionResult<IEnumerable<WeatherForecast>>>();

            var weathers = response.Result as OkObjectResult;
            
            var weatherForecasts = weathers?.Value as IEnumerable<WeatherForecast>;
            Assert.NotNull(weatherForecasts);
            var enumerable = weatherForecasts.ToArray();
            enumerable.Count().ShouldBe(3);
            enumerable.Select(x => x.Summary).ToArray().ShouldBeEquivalentTo(weatherList);
            enumerable.Select(f => f.TemperatureF).ToArray().Length.ShouldBe(3);
        }

        [Theory]
        [InlineData("one")]

        public void ShouldCreateWeather(string weather)
        {
            var response = _sut.Create(weather);

            response.ShouldBeAssignableTo<IActionResult>();

            var result = response as CreatedAtRouteResult;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status201Created);
            
            _weatherApiService.Received(1).AddWeather(Arg.Any<string>());
        }

        [Theory]
        [InlineData("new")]
        [InlineData("old")]
        [InlineData("older")]
        

        public void ShouldDeleteWeather(string weather)
        {
            _sut.Delete(weather);

            _weatherApiService.Received(1).DeleteWeather(weather);
        }

        [Theory]
        [InlineData("new")]
        [InlineData("old")]
        [InlineData("older")]

        public void ShouldGetWeatherByIndex(string weather)
        {
            _weatherApiService.GetWeatherByIndex(Arg.Any<int>()).Returns(weather);


            var response = _sut.GetByIndex(1);

            var result = response as OkObjectResult;

            result.ShouldNotBeNull();

            result.Value.ShouldBe(weather);
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            _weatherApiService.Received(1).GetWeatherByIndex(1);
        }

        [Theory]
        [InlineData("two", "three")]

        public async Task ShouldReplaceWeather(string oldValue, string newValue)
        {
            _uv.ValidateAsync(Arg.Any<UpdateWeatherRequest>()).Returns(Task.FromResult(new ValidationResult()));
            var response = await _sut.Update(new UpdateWeatherRequest(oldValue, newValue));

            response.ShouldNotBeNull();

            var result = response as NoContentResult;

            result.ShouldNotBeNull();

            _weatherApiService.Received(1).UpdateWeather(oldValue, newValue);
            await _uv.Received(1).ValidateAsync(new UpdateWeatherRequest(oldValue, newValue));
        }

        [Theory]
        [InlineData("two", "three")]

        public async Task ShouldReturnBadRequest(string oldValue, string newValue)
        {
            var errors = new List<ValidationFailure>
            {
                new("OldValue", "Old value is required"),
                new("NewValue", "New value must be at least 3 characters")
            };

            _uv.ValidateAsync(Arg.Any<UpdateWeatherRequest>()).Returns(Task.FromResult(new ValidationResult(errors)));
            var response = await _sut.Update(new UpdateWeatherRequest(oldValue, newValue));

            response.ShouldNotBeNull();

            var result = response as BadRequestObjectResult;

            result.ShouldNotBeNull();

            result.Value.ShouldNotBeNull();

            var value = result.Value as ValidationErrorResponse;
            value.ShouldNotBeNull();    

            value.Message.ShouldBe("Validation failed");
            value.Errors.Count.ShouldBe(2);
            var (field, message, code) = value.Errors[0];
            field.ShouldBe("OldValue");
            message.ShouldBe("Old value is required");
            code.ShouldBeNull();

            _weatherApiService.Received(0).UpdateWeather(oldValue, newValue);
            await _uv.Received(1).ValidateAsync(new UpdateWeatherRequest(oldValue, newValue));
        }
    }
}
