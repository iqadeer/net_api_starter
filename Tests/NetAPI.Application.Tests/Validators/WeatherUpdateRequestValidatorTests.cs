using AutoFixture;
using FluentValidation.TestHelper;
using NetAPI.Application.Dtos;
using NetAPI.Application.Validations;

namespace NetAPI.Application.Tests.Validators
{
    public class WeatherUpdateRequestValidatorTests
    {
        [Fact]
        public void ShouldValidateInvalidRequest()
        {
            var fixture = new Fixture();
            var request = fixture.Create<UpdateWeatherRequest>();
            var sut = new WeatherUpdateRequestValidator();
            var result = sut.TestValidate(request);
            Assert.NotNull(result);
            result.ShouldHaveValidationErrorFor(x => x.NewValue);
            result.ShouldHaveValidationErrorFor(x => x.OldValue);
        }

        [Fact]
        public void ShouldValidateValidRequest()
        {
            var fixture = new Fixture();
            var request = fixture.Build<UpdateWeatherRequest>()
                .With(x => x.NewValue, "new")
                .With(x=> x.OldValue, "old")
                .Create();
            var sut = new WeatherUpdateRequestValidator();
            var result = sut.TestValidate(request);
            Assert.NotNull(result);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
