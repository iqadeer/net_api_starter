using AutoFixture.Xunit2;
using NetAPI.API.Validations;
using NetAPI.Application.Dtos;
using Shouldly;

namespace NetAPI.API.Tests.Validators
{
    
    public class PersonDtoValidatorTests
    {
        [Theory, AutoData]
        public void ShouldValidatePersonDto(PersonDto dto)
        {
            dto.FirstName = "ali";
            dto.LastName = "naeem";

            dto.TermsAccepted = true;

            var sut = new PersonDtoValidator();
            var result = sut.Validate(dto);

            result.IsValid.ShouldBeTrue();
        }

        [Theory, AutoData]
        public void ShouldInValidatePersonDtoTerms(PersonDto dto)
        {
            dto.FirstName = "ali";
            dto.LastName = "naeem";

            dto.TermsAccepted = false;

            var sut = new PersonDtoValidator();
            var result = sut.Validate(dto);

            result.IsValid.ShouldBeFalse();
            result.Errors.First().ErrorMessage.ShouldBe("'Terms Accepted' must not be empty.");
        }

        [Theory, AutoData]
        public void ShouldInValidatePersonDtoFirstName(PersonDto dto)
        {
            dto.FirstName = new string('a', 61);
            dto.LastName = "naeem";

            dto.TermsAccepted = true;

            var sut = new PersonDtoValidator();
            var result = sut.Validate(dto);

            result.IsValid.ShouldBeFalse();
            result.Errors.First().ErrorMessage.ShouldBe("The length of 'First Name' must be 60 characters or fewer. You entered 61 characters.");
        }

        [Theory, AutoData]
        public void ShouldInValidatePersonDtoLastName(PersonDto dto)
        {
            dto.FirstName = "ali";
            dto.LastName = new string('a', 61);

            dto.TermsAccepted = true;

            var sut = new PersonDtoValidator();
            var result = sut.Validate(dto);

            result.IsValid.ShouldBeFalse();
            result.Errors.First().ErrorMessage.ShouldBe("The length of 'Last Name' must be 60 characters or fewer. You entered 61 characters.");
        }
    }
}
