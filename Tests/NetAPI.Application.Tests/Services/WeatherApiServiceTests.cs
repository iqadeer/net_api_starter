using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
using NetAPI.Application.Services;
using NetAPI.Domain.Entities;
using NSubstitute;
using Shouldly;

namespace NetAPI.Application.Tests.Services
{
    public class WeatherApiServiceTests
    {
        private readonly List<string> _summaries =
            ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

        [Fact]
        public void ShouldBeOfAssignableTo()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.ShouldBeAssignableTo<IWeatherApiService>();
        }

        [Fact]
        public void ShouldGetWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(10);
            weather.ShouldContain("Warm");
        }

        [Fact]
        public void ShouldAddWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.AddWeather("New_Weather");
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(11);
            weather.ShouldContain("New_Weather");
        }

        [Fact]
        public void ShouldDeleteWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.DeleteWeather("Cool");
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(9);
            weather.ShouldNotContain("Cool");
        }

        [Fact]
        public void ShouldUpdateWeather()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var sut = new WeatherApiService(mockedLogger, _summaries);
            sut.UpdateWeather("Cool", "Cooler");
            var weather = sut.GetWeather();
            weather.ShouldNotBeNull();
            weather.Length.ShouldBe(10);
            weather.ShouldNotContain("Cool");
            weather.ShouldContain("Cooler");
        }

        [Theory]
        [InlineData(0, "zero")]
        [InlineData(1, "one")]
        [InlineData(3, "three")]
        [InlineData(9, "ten")]
        public void GetWeatherByIndex(int index, string value)
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mockedLogger = fixture.Create<ILogger<WeatherApiService>>();
            var weatherList = _summaries;
            weatherList[index] = value;

            var sut = new WeatherApiService(mockedLogger, weatherList);
            var weathers = sut.GetWeather();
            weathers.Length.ShouldBe(10);
            var weather = sut.GetWeatherByIndex(index);
            weather.ShouldNotBeNull();
            weather.ShouldBe(value);
        }
    }

    public class PersonServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepo _repo;

        public PersonServiceTests()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _repo = fixture.Create<IPersonRepo>();
            _mapper = fixture.Create<IMapper>();

        }

        [Theory, AutoData]
        public void ShouldGetPersons(List<Person> persons, List<PersonDto> dtos)
        {
            _repo.GetPersons().Returns(persons.ToArray());
            _mapper.Map<PersonDto[]>(Arg.Any<Person[]>()).Returns(dtos.ToArray());

            var sut = new PersonService(_repo, _mapper);

            var personsResponse = sut.GetPersons();
            personsResponse.ShouldNotBeNull();
         
            personsResponse.Length.ShouldBe(dtos.Count);
            personsResponse.ShouldBeEquivalentTo(dtos.ToArray());

            _repo.Received(1).GetPersons();
        }

        [Theory, AutoData]
        public void ShouldGetPersonById(Person person, PersonDto dto)
        {
            _repo.GetPerson(Arg.Any<int>()).Returns(person);
            _mapper.Map<PersonDto>(Arg.Any<Person>()).Returns(dto);

            var sut = new PersonService(_repo, _mapper);

            var personResponse = sut.GetPerson(2);
            personResponse.ShouldNotBeNull();

            personResponse.ShouldBeEquivalentTo(dto);

            _repo.Received(1).GetPerson(2);
        }


        [Theory, AutoData]
        public void ShouldAddPerson(Person person, PersonDto dto)
        {
            _repo.AddPerson(Arg.Any<Person>()).Returns(2);
            _mapper.Map<Person>(Arg.Any<PersonDto>()).Returns(person);

            var sut = new PersonService(_repo, _mapper);

            var personResponse = sut.AddPerson(dto);
            personResponse.ShouldBe(2);

            _repo.Received(1).AddPerson(person);
        }
    }
}
