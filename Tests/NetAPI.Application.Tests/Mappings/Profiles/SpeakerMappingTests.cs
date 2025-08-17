using AutoFixture.Xunit2;
using AutoMapper;
using NetAPI.Application.Dtos;
using NetAPI.Application.Mappings;
using NetAPI.Domain.Entities;
using Shouldly;

namespace NetAPI.Application.Tests.Mappings.Profiles
{
    public class SpeakerMappingTests
    {
        private readonly IMapper _mapper;

        public SpeakerMappingTests()
        {
            var cfg = new MapperConfiguration(cfg =>
                cfg.AddProfile<SpeakerProfile>());

            cfg.AssertConfigurationIsValid();

            _mapper = cfg.CreateMapper();
        }

        [Theory, AutoData]

        public void ShouldMapSpeakerToSpeakerDto(Speaker speaker)
        {
            var speakerDto = _mapper.Map<SpeakerDto>(speaker);

            speakerDto.Email.ShouldBe(speaker.Email);
            speakerDto.Country.ShouldBe(speaker.Country);
            speakerDto.Gender.ShouldBe(speaker.Gender);
            speakerDto.Id.ShouldBe(speaker.Id);
            speakerDto.Name.ShouldBe(speaker.Name);

        }
    }
}
