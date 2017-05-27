using ClientMainServiceAPI.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Contracts;
using System;
using System.Web.Http;

namespace ClientMainServiceAPI.Controllers
{
    [RoutePrefix("api/ClientMain")]
    public class ClientMainController : ApiController, IClientMain
    {
        private IPersonModel _model;

        public ClientMainController(IPersonModel model)
        {
            this._model = model;
        }

        [AcceptVerbs("GET")]
        [Route("GetByID/{id}")]
        public IHttpActionResult GetByID(string Id)
        {
            Person result = null;
            try
            {
                result = _model.GetById(Id);
            }catch(Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}
