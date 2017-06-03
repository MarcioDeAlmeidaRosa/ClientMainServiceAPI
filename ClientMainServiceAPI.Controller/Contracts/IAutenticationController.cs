using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Domain.Model;

namespace ClientMainServiceAPI.Controller.Contracts
{
    public interface IAutenticationController
    {
        void Register(User entity);
        void Confirm(string valeu, string token);
        ResultAutentication LoginExternalAuthentication(User user);
        ResultAutentication LinkExternalAuthentication(LinkUser user);
        ResultAutentication Login(User entity);
    }
}
