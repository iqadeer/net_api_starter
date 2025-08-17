using AutoFixture.Xunit2;
using NetAPI.Application.Dtos;
using NetAPI.Application.Validations;
using Shouldly;

namespace NetAPI.Application.Tests.Validators
{
    public class SpeakerValidatorTests
    {
        private readonly SpeakerDtoValidator _validator = new();

        [Theory, AutoData]
        public async Task ShouldValidateValidData(SpeakerDto speakerDto)
        {
            speakerDto.Email = "im@d.dk";
            var result = await _validator.ValidateAsync(speakerDto);
            result.ShouldNotBeNull();
            result.IsValid.ShouldBeTrue();
        }

        [Theory, AutoData]
        public async Task ShouldInValidateInValidData(SpeakerDto speakerDto)
        {
            speakerDto.Email = "a";
            var result = await _validator.ValidateAsync(speakerDto);

            
            result.ShouldNotBeNull();
            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].PropertyName.ShouldBe("Email");
        }
    }
}
