using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentValidation;
using NetAPI.API.Controllers;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
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
            //response.ShouldBe(person);
        }

        [Fact]
        public void ShouldReturnAllPerson()
        {
            var persons = _fixture.Create<List<PersonDto>>();
            _personService.GetPersons().Returns(persons.ToArray());

            var response = _sut.Get();

            response.ShouldNotBeNull();
            // response.ShouldBe(persons);
        }
    }
}
