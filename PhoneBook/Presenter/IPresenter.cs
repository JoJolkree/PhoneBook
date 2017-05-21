using System;
using System.Collections.Generic;
using PhoneBook.Model;

namespace PhoneBook.Presenter
{
    public interface IPresenter : IDisposable
    {
        IEnumerable<Person> GetListOfPersons();
        IEnumerable<Person> GetListOfPersons(string query);
        void DeletePerson(int id);
        void AddPerson(Person person);
        void UpdatePerson(int id, Person newPersonInfo);
        Person GetPersonFromId(int id);
    }
}