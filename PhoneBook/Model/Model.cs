using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Model
{
    public class Model : IModel
    {
        private readonly PersonContext _personsDb;

        public Model(PersonContext personsDb)
        {
            this._personsDb = personsDb;
        }

        public IEnumerable<Person> GetListOfPersons()
        {
            foreach (var person in _personsDb.Persons)
                yield return person;
        }

        public IEnumerable<Person> GetListOfPersons(string query)
        {
            var persons = _personsDb.Persons.Where(x => x.LastName.Contains(query)
                                                       || x.FirstName.Contains(query)
                                                       || x.PhoneNumber.Contains(query)
                                                       || x.Email.Contains(query));
            foreach (var person in persons)
                yield return person;
        }

        public void DeletePerson(int id)
        {
            var toRemove = _personsDb.Persons.SingleOrDefault(x => x.Id == id);

            if (toRemove != null)
                _personsDb.Persons.Remove(toRemove);
            _personsDb.SaveChanges();
        }

        public void AddPerson(Person person)
        {
            _personsDb.Persons.Add(person);
            _personsDb.SaveChanges();
        }

        public void UpdatePerson(int id, Person newPersonInfo)
        {
            var person = _personsDb.Persons.SingleOrDefault(x => x.Id == id);
            if (person == null) return;
            person.CopyInfoFromOtherPerson(newPersonInfo);
            _personsDb.SaveChanges();
        }

        public Person GetPersonFromId(int id)
        {
            return _personsDb.Persons.SingleOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            _personsDb?.Dispose();
        }
    }
}