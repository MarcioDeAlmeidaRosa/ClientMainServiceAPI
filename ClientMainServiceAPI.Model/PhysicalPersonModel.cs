using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.DB;

namespace ClientMainServiceAPI.Model
{
    public class PhysicalPersonModel : DBFactory<PhysicalPerson>
    {
        public PhysicalPersonModel() : base("client-api", "Persons")
        {
        }
    }
}
