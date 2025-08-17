using NetAPI.Application.Interfaces;
using NetAPI.Domain.Entities;

namespace NetAPI.Infrastructure.Repos
{
    public class PersonRepo: IPersonRepo
    {
        private readonly List<Person> _persons = new();
        private int _nextId = 1;
        public Person[] GetPersons()
        {
            return _persons.ToArray();
        }

        public int AddPerson(Person person)
        {
            person.Id = _nextId;
            _persons.Add(person);
            _nextId++;
            return person.Id.Value;
        }

        public Person? GetPerson(int id)
        {
            var person = _persons.SingleOrDefault(p => p.Id == id);
            return person;
        }
    }
}