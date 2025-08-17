using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetAPI.API.Validations;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;

namespace NetAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherApiService weatherApiService,
        IValidator<UpdateWeatherRequest> uv) : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var weathers = weatherApiService.GetWeather();
            var result = weathers.Select((val, index) => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = val
            })
            .ToArray();
            return Ok(result);
        }

        [HttpPost(Name = "AddWeather")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] string weather)
        {
            var weathers = weatherApiService.GetWeather();
            weatherApiService.AddWeather(weather);
            return CreatedAtRoute("GetByIndex", new { index = weathers.Length });
        }

        [HttpDelete(Name = "DeleteWeather")]
        public IActionResult Delete([FromBody] string weather)
        {
            weatherApiService.DeleteWeather(weather);
            return NoContent();
        }

        [HttpGet("{index:int}", Name = "GetByIndex")]
        public IActionResult GetByIndex([FromRoute] int index)
        {
            return Ok(weatherApiService.GetWeatherByIndex(index));
        }

        [HttpPut(Name = "Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateWeatherRequest request)
        {
            var vr = await uv.ValidateAsync(request);
            if (vr.IsValid)
            {
                var (oldValue, newValue) = request;
                weatherApiService.UpdateWeather(oldValue, newValue);
                return NoContent();
            }
            else
            {
                var errors = vr.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage, x.ErrorCode));
                return BadRequest(new ValidationErrorResponse("Validation failed", errors.ToList()));
            }
        }
    }
}
