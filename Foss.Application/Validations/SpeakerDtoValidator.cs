using FluentValidation;
using NetAPI.Application.Dtos;

namespace NetAPI.Application.Validations;

public class SpeakerDtoValidator : AbstractValidator<SpeakerDto>
{
    public SpeakerDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
    }
}