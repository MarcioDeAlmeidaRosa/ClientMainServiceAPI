using ClientMainServiceAPI.Domain;

namespace ClientMainServiceAPI.Controller.Contracts
{
    public interface IPersonController
    {
        Person GetById(string Id);

        Person CreatePerson(Person person);
    }
}
