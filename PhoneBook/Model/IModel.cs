using System;
using System.Collections.Generic;

namespace PhoneBook.Model
{
    public interface IModel : IDisposable
    {
        IEnumerable<Person> GetListOfPersons();
        IEnumerable<Person> GetListOfPersons(string query);
        void DeletePerson(int id);
        void AddPerson(Person person);
        void UpdatePerson(int id, Person newPersonInfo);
        Person GetPersonFromId(int id);
    }
}