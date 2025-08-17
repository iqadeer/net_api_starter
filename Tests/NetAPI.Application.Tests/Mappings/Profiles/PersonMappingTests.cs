using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using NetAPI.Application.Dtos;
using NetAPI.Application.Mappings;
using NetAPI.Domain.Entities;
using Shouldly;

// Or NUnit

namespace NetAPI.Application.Tests.Mappings.Profiles;

public class PersonProfileTests
{
    private readonly IMapper _mapper;
    private readonly IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

    public PersonProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PersonProfile>();
        });

        config.AssertConfigurationIsValid();

        _mapper = config.CreateMapper();
        _fixture.Customize<DateOnly>(cfg =>
            cfg.FromFactory(() => DateOnly.FromDateTime(new DateTime(2024, 10, 10))));
    }

    [Fact]
    public void PersonToPersonDto_ShouldMap_FullName_From_Name()
    {
        // Arrange
        var person = _fixture.Create<Person>();
        //var person = new Person(Id: 1, Name: "John Doe", Gender: 0,
        //    Dob: new DateOnly(1990, 1, 1), new DateTimeOffset(new DateTime(2012, 10, 10)));

        // Act
        var dto = _mapper.Map<PersonDto>(person);

        // Assert
        dto.ShouldNotBeNull();
        dto.FirstName.ShouldBe(person.FirstName);
        dto.LastName.ShouldBe(person.LastName);
        dto.Id.ShouldBe(person.Id);
        dto.Gender.ShouldBe(person.Gender);
        dto.Dob.ShouldBe(person.Dob);
    }

    [Fact]
    public void PersonDtoToPerson_ShouldMap_Name_Correctly()
    {
        // Arrange
        var dto = _fixture.Create<PersonDto>();
        //var dto = new PersonDto(Id: 1, "John Doe", 0, new DateOnly(1990, 1, 1), "John Doe", new DateTimeOffset(new DateTime(2012, 10, 10)));

        // Act
        var person = _mapper.Map<Person>(dto);

        // Assert
        person.ShouldNotBeNull();
        person.FirstName.ShouldBe(dto.FirstName);
        dto.LastName.ShouldBe(person.LastName);
        person.Id.ShouldBe(dto.Id);
        person.Gender.ShouldBe(dto.Gender);
        person.Dob.ShouldBe(dto.Dob);
    }
}