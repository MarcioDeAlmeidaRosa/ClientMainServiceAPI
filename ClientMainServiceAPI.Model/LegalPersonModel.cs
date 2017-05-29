using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.DB;

namespace ClientMainServiceAPI.Model
{
    public class LegalPersonModel : DBFactory<LegalPerson>
    {
        public LegalPersonModel() : base("client-api", "Persons")
        {
        }
    }
}
