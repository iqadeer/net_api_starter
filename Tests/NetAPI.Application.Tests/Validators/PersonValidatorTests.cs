using AutoFixture.Xunit2;
using NetAPI.API.Validations;
using NetAPI.Application.Dtos;

namespace NetAPI.Application.Tests.Validators
{
    
    public class PersonDtoValidatorTests
    {
        [Theory, AutoData]
        public void ShouldValidatePersonDto(PersonDto dto)
        {
            dto.FirstName = "ali";
            dto.LastName = "naeem";

            dto.TermsAccepted = false;

            var sut = new PersonDtoValidator();
            //var sut = new PersonDtoValidator();
            //var result = sut.Validate(dto);

            //result.IsValid.ShouldBeTrue();
        }
    }
}
