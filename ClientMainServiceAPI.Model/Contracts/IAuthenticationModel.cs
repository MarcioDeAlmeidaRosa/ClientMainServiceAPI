using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Domain.Model;

namespace ClientMainServiceAPI.Model.Contracts
{
    public interface IAuthenticationModel
    {
        void Register(User entity);
        void ConfirmEmail(string id, string token);
        ResultAuthentication LoginExternalAuthentication(User entity);
        ResultAuthentication LinkExternalAuthentication(LinkUser user);
        ResultAuthentication Login(User entity);
    }
}
