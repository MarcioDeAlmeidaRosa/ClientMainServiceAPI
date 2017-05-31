using ClientMainServiceAPI.Domain;

namespace ClientMainServiceAPI.Model.Contracts
{
    public interface IUserConnectedModel
    {
        UserConnected FindByToken(string token);
    }
}
