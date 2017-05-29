using System;
using ClientMainServiceAPI.Controller.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model;

namespace ClientMainServiceAPI.Controller
{
    public class PersonController : IPersonModel
    {
        private PersonModel _model;

        //TODO - VOLTAR DEPENDÊNCIA
        //public PersonController(PersonModel model)
        //{
        //    this._model = model;
        //}

        public PersonController()
        {
            _model = new PersonModel("client-api", "Persons");
        }

        public Person CreatePerson(Person person)
        {
            return _model.Create(person);
        }

        public Person GetById(string Id)
        {
            return _model.GetById(Id);
        }
    }
}
