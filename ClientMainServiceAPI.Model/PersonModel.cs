using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.DB;

namespace ClientMainServiceAPI.Model
{
    public class PersonModel : DBFactory<Person>
    {
        public PersonModel(string dataBaseName, string collectionName) : base(dataBaseName, collectionName)
        {
        }
    }
}
