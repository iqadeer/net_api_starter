using FluentValidation;
using NetAPI.Application.Dtos;

namespace NetAPI.API.Validations
{
    public record ValidationErrorResponse(string Message, List<ValidationError> Errors);

    public record ValidationError(string Field, string Error, string ErrorCode)
    {

    }
    public class WeatherUpdateRequestValidator : AbstractValidator<UpdateWeatherRequest>
    {
        public WeatherUpdateRequestValidator()
        {
            RuleFor(x => x.NewValue).NotEmpty().MinimumLength(2).MaximumLength(15);
            RuleFor(x => x.OldValue).NotEmpty().MinimumLength(1).MaximumLength(15);
        }
    }
}
