using ClientMainServiceAPI.Controller.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model;
using ClientMainServiceAPI.Domain.Model;
using ClientMainServiceAPI.Model.Contracts;

namespace ClientMainServiceAPI.Controller
{
    public class AutenticationController : IAutenticationController
    {
        private IAutenticationModel _model;

        //TODO - VOLTAR DEPENDÊNCIA
        //public AutenticationController(IAutenticationModel model)
        //{
        //    this._model = model;
        //}

        public AutenticationController()
        {
            _model = new AutenticationModel();
        }

        public void Register(User user)
        {
            _model.Register(user);
        }

        public void Confirm(string valeu, string token)
        {
            _model.ConfirmEmail(valeu, token);
        }

        public ResultAutentication LoginExternalAuthentication(User user)
        {
            return _model.LoginExternalAuthentication(user);
        }

        public ResultAutentication LinkExternalAuthentication(LinkUser user)
        {
            return _model.LinkExternalAuthentication(user);
        }

        public ResultAutentication Login(User entity)
        {
            return _model.Login(entity);
        }
    }
}
