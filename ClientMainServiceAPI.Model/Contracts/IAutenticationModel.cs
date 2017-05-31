using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Domain.Model;

namespace ClientMainServiceAPI.Model.Contracts
{
    public interface IAutenticationModel
    {
        void Register(User entity);
        void ConfirmEmail(string id, string token);
        ResultAutentication LoginExternalAuthentication(User entity);
        ResultAutentication LinkExternalAuthentication(LinkUser user);
    }
}
