using FluentValidation;
using NetAPI.Application.Dtos;

namespace NetAPI.API.Validations;

public class PersonDtoValidator : AbstractValidator<PersonDto>
{
    public PersonDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(60);
        RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(60);

        RuleFor(x => x.TermsAccepted).NotEmpty().NotNull();
    }
}