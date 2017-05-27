using System.Web.Http;

namespace ClientMainServiceAPI.Contracts
{
    public interface IClientMain
    {
        IHttpActionResult GetByID(string Id);
    }
}