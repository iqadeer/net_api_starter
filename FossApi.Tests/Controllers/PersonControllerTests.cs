using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NetAPI.API.Controllers;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
using Newtonsoft.Json.Linq;
using NSubstitute;
using Shouldly;

namespace NetAPI.API.Tests.Controllers
{
    public class PersonControllerTests
    {
        private readonly PersonController _sut;
        private readonly IPersonService _personService;
        private readonly IValidator<PersonDto> _validator;
        private readonly IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        public PersonControllerTests()
        {
            _personService = _fixture.Freeze<IPersonService>();
            _validator = _fixture.Freeze<IValidator<PersonDto>>();
            _sut = new PersonController(_personService, _validator);
            _fixture.Customize<DateOnly>(c =>
                c.FromFactory(() => DateOnly.FromDateTime(DateTime.Now.AddDays(_fixture.Create<int>() % 365))));
        }

        [Fact]
        public void ShouldReturnAPerson()
        {
            var person = _fixture.Create<PersonDto>();
            _personService.GetPerson(Arg.Any<int>()).Returns(person);

            var response = _sut.Get(2);

            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();

            var result = response as OkObjectResult;
            result.ShouldNotBeNull();
            result.Value.ShouldBe(person);
        }

        [Fact]
        public void ShouldReturnAllPerson()
        {
            var persons = _fixture.Create<List<PersonDto>>();
            _personService.GetPersons().Returns(persons.ToArray());

            var response = _sut.Get();

            response.ShouldNotBeNull();
            response.ShouldBeOfType<OkObjectResult>();

            var result = response as OkObjectResult;
            result.ShouldNotBeNull();
            result.Value.ShouldBe(persons);

        }

        [Fact]
        public async Task ShouldCreatePerson()
        {
            var person = _fixture.Create<PersonDto>();
            _personService.AddPerson(Arg.Any<PersonDto>()).Returns(2);
            _validator.ValidateAsync(Arg.Any<PersonDto>()).Returns(new ValidationResult());
            var response = await _sut.Post(person);

            response.ShouldNotBeNull();
            response.ShouldBeOfType<CreatedAtActionResult>();

            var result = response as CreatedAtActionResult;
            result.ShouldNotBeNull();
            dynamic val = result.Value!;
            var jObject = JObject.FromObject(result.Value!);
            int id = jObject["Id"]!.Value<int>();
            id.ShouldBe(2);

        }

        [Fact]
        public async Task ShouldNotCreatePerson()
        {
            var person = _fixture.Create<PersonDto>();
            _personService.AddPerson(Arg.Any<PersonDto>()).Returns(2);
            _validator.ValidateAsync(Arg.Any<PersonDto>()).Returns(new ValidationResult(
                [new ValidationFailure("Prop", "Error")]));
            var response = await _sut.Post(person);

            response.ShouldNotBeNull();
            response.ShouldBeOfType<BadRequestObjectResult>();

            var result = response as BadRequestObjectResult;
            result.ShouldNotBeNull();
            dynamic val = result.Value!;
            var jObject = JObject.FromObject(result.Value!);
            string message= jObject["Message"]!.Value<string>();
            message.ShouldBe("Validation failed for speaker");
        }


    }
}
