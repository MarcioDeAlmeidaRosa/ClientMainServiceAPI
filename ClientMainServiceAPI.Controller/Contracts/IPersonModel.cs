using ClientMainServiceAPI.Domain;

namespace ClientMainServiceAPI.Controller.Contracts
{
    public interface IPersonModel
    {
        Person GetById(string Id);

        Person CreatePerson(Person person);
    }
}
