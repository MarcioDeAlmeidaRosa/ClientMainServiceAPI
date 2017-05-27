using ClientMainServiceAPI.Domain;

namespace ClientMainServiceAPI.Model.Contracts
{
    public interface IPersonModel
    {
        Person GetById(string Id);

        Person CreatePerson(Person person);
    }
}
