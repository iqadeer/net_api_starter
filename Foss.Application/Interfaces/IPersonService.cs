using NetAPI.Application.Dtos;
using NetAPI.Domain.Entities;

namespace NetAPI.Application.Interfaces
{
    public interface IPersonService
    {
        PersonDto[] GetPersons();
        int AddPerson(PersonDto person);
        PersonDto? GetPerson(int id);
    }
}
