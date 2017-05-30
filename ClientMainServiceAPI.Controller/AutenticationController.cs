using System;
using ClientMainServiceAPI.Controller.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model;

namespace ClientMainServiceAPI.Controller
{
    public class AutenticationController : IAutenticationController
    {
        private AutenticationModel _model;

        //TODO - VOLTAR DEPENDÊNCIA
        //public AutenticationController(AutenticationModel model)
        //{
        //    this._model = model;
        //}

        public AutenticationController()
        {
            _model = new AutenticationModel();
        }

        public void Create(User user)
        {
            _model.Create(user);
        }

        public void Confirm(string valeu)
        {
            _model.ConfirmEmail(valeu);
        }
    }
}
