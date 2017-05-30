using ClientMainServiceAPI.Domain;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClientMainServiceAPI.Contracts
{
    public interface IAutentication
    {
        Task<IHttpActionResult> Register([FromBody] User user);
    }
}