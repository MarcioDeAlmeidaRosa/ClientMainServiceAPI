using ClientMainServiceAPI.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Controller.Contracts;
using System;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetByID(string Id)
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

        [AcceptVerbs("POST")]
        [Route("CreatePhysicalClient")]
        public async Task<IHttpActionResult> CreatePhysicalClient([FromBody] PhysicalPerson person)
        {
            Person result = null;
            try
            {
                result = _model.CreatePerson(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [AcceptVerbs("POST")]
        [Route("CreateLegalClient")]
        public async Task<IHttpActionResult> CreateLegalClient([FromBody] LegalPerson person)
        {
            Person result = null;
            try
            {
                result = _model.CreatePerson(person);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}
