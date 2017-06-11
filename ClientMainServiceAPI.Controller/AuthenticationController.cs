using ClientMainServiceAPI.Controller.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model;
using ClientMainServiceAPI.Domain.Model;
using ClientMainServiceAPI.Model.Contracts;

namespace ClientMainServiceAPI.Controller
{
    public class AuthenticationController : IAuthenticationController
    {
        private IAuthenticationModel _model;

        //TODO - VOLTAR DEPENDÊNCIA
        //public AutenticationController(IAutenticationModel model)
        //{
        //    this._model = model;
        //}

        public AuthenticationController()
        {
            _model = new AuthenticationModel();
        }

        public void Register(User user)
        {
            _model.Register(user);
        }

        public void Confirm(string valeu, string token)
        {
            _model.ConfirmEmail(valeu, token);
        }

        public ResultAuthentication LoginExternalAuthentication(User user)
        {
            return _model.LoginExternalAuthentication(user);
        }

        public ResultAuthentication LinkExternalAuthentication(LinkUser user)
        {
            return _model.LinkExternalAuthentication(user);
        }

        public ResultAuthentication Login(User entity)
        {
            return _model.Login(entity);
        }
    }
}
