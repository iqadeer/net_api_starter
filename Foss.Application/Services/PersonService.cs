using AutoMapper;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;
using NetAPI.Domain.Entities;
using NetAPI.Domain.Interfaces;

namespace NetAPI.Application.Services
{
    public class PersonService(IPersonRepo repo, IMapper mapper) : IPersonService
    {
        public PersonDto[] GetPersons()
        {
            return mapper.Map<PersonDto[]>(repo.GetPersons());
        }

        public int AddPerson(PersonDto person)
        {
            return repo.AddPerson(mapper.Map<Person>(person));
        }

        public PersonDto? GetPerson(int id)
        {
            return mapper.Map<PersonDto>(repo.GetPerson(id));
        }
    }
}
