using NetAPI.Domain.Entities;

namespace NetAPI.Application.Interfaces
{
    public interface IPersonRepo
    {
        public Person[] GetPersons();
        public int AddPerson(Person person);
        Person? GetPerson(int id);
    }
}
