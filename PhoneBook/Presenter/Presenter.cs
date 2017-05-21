using System.Collections.Generic;
using PhoneBook.Model;

namespace PhoneBook.Presenter
{
    public class Presenter : IPresenter
    {
        private readonly IModel _model;

        public Presenter(IModel model)
        {
            _model = model;
        }

        public IEnumerable<Person> GetListOfPersons()
        {
            return _model.GetListOfPersons();
        }

        public IEnumerable<Person> GetListOfPersons(string query)
        {
            return _model.GetListOfPersons(query);
        }

        public void DeletePerson(int id)
        {
            _model.DeletePerson(id);
        }

        public void AddPerson(Person person)
        {
            _model.AddPerson(person);
        }

        public void UpdatePerson(int id, Person newPersonInfo)
        {
            _model.UpdatePerson(id, newPersonInfo);
        }

        public Person GetPersonFromId(int id)
        {
            return _model.GetPersonFromId(id);
        }

        public void Dispose()
        {
            _model?.Dispose();
        }
    }
}