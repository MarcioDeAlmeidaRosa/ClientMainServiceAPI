using System;
using System.Web.Http;

namespace ClientMainServiceAPI.Controllers
{
    [RoutePrefix("api/ClientMain")]
    public class ClientMainController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("GetByID/{id}")]
        public IHttpActionResult GetByID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
