using AutoMapper;
using NetAPI.Application.Dtos;
using NetAPI.Domain.Entities;

namespace NetAPI.Application.Mappings;

public class SpeakerProfile : Profile
{
    public SpeakerProfile()
    {
        CreateMap<Speaker, SpeakerDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ReverseMap();
    }
}