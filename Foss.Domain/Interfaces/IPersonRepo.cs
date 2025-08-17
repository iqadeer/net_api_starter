using NetAPI.Domain.Entities;

namespace NetAPI.Domain.Interfaces
{
    public interface IPersonRepo
    {
        public Person[] GetPersons();
        public int AddPerson(Person person);
        Person? GetPerson(int id);
    }
}
