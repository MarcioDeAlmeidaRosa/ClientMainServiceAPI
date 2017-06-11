using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Domain.Model;

namespace ClientMainServiceAPI.Controller.Contracts
{
    public interface IAuthenticationController
    {
        void Register(User entity);
        void Confirm(string valeu, string token);
        ResultAuthentication LoginExternalAuthentication(User user);
        ResultAuthentication LinkExternalAuthentication(LinkUser user);
        ResultAuthentication Login(User entity);
    }
}
