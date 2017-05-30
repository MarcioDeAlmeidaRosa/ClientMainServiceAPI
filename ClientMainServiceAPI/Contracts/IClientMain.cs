using ClientMainServiceAPI.Domain;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClientMainServiceAPI.Contracts
{
    public interface IClientMain
    {
        Task<IHttpActionResult> GetByID(string Id);

        Task<IHttpActionResult> CreatePhysicalClient([FromBody] PhysicalPerson  person);

        Task<IHttpActionResult> CreateLegalClient([FromBody] LegalPerson person);
    }
}