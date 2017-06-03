using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Domain.Model;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClientMainServiceAPI.Contracts
{
    public interface IAutentication
    {
        Task<IHttpActionResult> Register([FromBody] User user);
        Task<IHttpActionResult> LoginExternalAuthentication([FromBody] User user);
        Task<IHttpActionResult> LinkExternalAuthentication([FromBody] LinkUser user);
        Task<IHttpActionResult> Login([FromBody] User user);
    }
}