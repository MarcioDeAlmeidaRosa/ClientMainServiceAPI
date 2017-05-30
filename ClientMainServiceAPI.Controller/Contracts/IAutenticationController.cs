using ClientMainServiceAPI.Domain;

namespace ClientMainServiceAPI.Controller.Contracts
{
    public interface IAutenticationController
    {
        void Create(User user);
        void Confirm(string valeu);
    }
}
