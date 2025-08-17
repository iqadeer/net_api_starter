using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NetAPI.API.Controllers;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
using NSubstitute;
using Shouldly;

namespace NetAPI.API.Tests.Controllers
{
    public class SpeakersControllerTests
    {
        private readonly IFixture _fixture;
        private readonly SpeakersController _sut;
        private readonly ISpeakerService _speakerService;
        private readonly IValidator<SpeakerDto> _validator;
        public SpeakersControllerTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fixture.Customize<DateOnly>(c =>
                c.FromFactory(() => DateOnly.FromDateTime(DateTime.Now.AddDays(_fixture.Create<int>() % 365))));
            _speakerService = _fixture.Create<ISpeakerService>();

            _validator = _fixture.Create<IValidator<SpeakerDto>>();
            _sut = new SpeakersController(_speakerService, _validator);
        }

        [Fact]
        public void ShouldBeOfType()
        {
            _sut.ShouldBeAssignableTo<ControllerBase>();
        }

        [Fact]
        public void ShouldReturnSpeakers()
        {
            var speakers = _fixture.Create<SpeakerDto[]>();
            _speakerService.GetAll().Returns(speakers);
            var response = _sut.Get();
            response.ShouldNotBeNull();

            response.ShouldBeOfType<OkObjectResult>();

            var result = response as OkObjectResult;
            result.ShouldNotBeNull();
            result.Value.ShouldBe(speakers);
        }

        [Theory, AutoData]
        
        public void ShouldReturnSpeaker(int id, SpeakerDto speaker)
        {
            _speakerService.Get(Arg.Any<int>()).Returns(speaker);
            var response = _sut.Get(id);
            response.ShouldNotBeNull();

            response.ShouldBeOfType<OkObjectResult>();

            var result = response as OkObjectResult;
            result.ShouldNotBeNull();
            result.Value.ShouldBe(speaker);
        }

        [Theory, AutoData]

        public void ShouldThrow(int id, SpeakerDto speaker)
        {
            _speakerService.Get(Arg.Any<int>()).Returns(speaker);
            var act = () => _sut.Get(0);
            var exception = Should.Throw<ArgumentException>(act);
            exception.ShouldNotBeNull();
            exception.Message.ShouldBe("Speaker id is not in range (Parameter 'speakerId')");
        }

        [Theory, AutoData]

        public async Task ShouldCreate(int id, SpeakerDto speaker)
        {
            _speakerService.Create(Arg.Any<SpeakerDto>()).Returns(id);
            _validator.ValidateAsync(Arg.Any<SpeakerDto>()).Returns(Task.FromResult(new ValidationResult()));
            var response = await _sut.Post(speaker);
            response.ShouldNotBeNull();
            var result = response.ShouldBeOfType<CreatedAtRouteResult>();
            result.RouteName.ShouldBe("");
            result.RouteValues.ShouldBe(null);
            var speakerIdProperty = result.Value?.GetType().GetProperty("speakerId");
            var speakerId = (int)(speakerIdProperty?.GetValue(result.Value) ?? -1);
            speakerId.ShouldBe(id);
        }

        [Theory, AutoData]

        public async Task ShouldReturnBadRequest(int id, SpeakerDto speaker)
        {
            var errors = new List<ValidationFailure>
            {
                new("OldValue", "Old value is required"),
                new("NewValue", "New value must be at least 3 characters")
            };

            _speakerService.Create(Arg.Any<SpeakerDto>()).Returns(id);
            _validator.ValidateAsync(Arg.Any<SpeakerDto>()).Returns(Task.FromResult(new ValidationResult(errors)));
            var response = await _sut.Post(speaker);
            response.ShouldNotBeNull();
            var result = response.ShouldBeOfType<BadRequestObjectResult>();
            //var speakerIdProperty = result.Value.GetType().GetProperty("speakerId");
            //var speakerId = (int)speakerIdProperty.GetValue(result.Value);
            //speakerId.ShouldBe(id);
        }


    }
}
