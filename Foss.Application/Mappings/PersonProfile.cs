using AutoMapper;
using NetAPI.Application.Dtos;
using NetAPI.Domain.Entities;

namespace NetAPI.Application.Mappings
{
    public class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName) )
                .ReverseMap();
        }
    }
}
