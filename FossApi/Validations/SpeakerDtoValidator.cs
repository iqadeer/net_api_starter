using FluentValidation;
using NetAPI.Application.Dtos;

namespace NetAPI.API.Validations;

public class SpeakerDtoValidator : AbstractValidator<SpeakerDto>
{
    public SpeakerDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
    }
}