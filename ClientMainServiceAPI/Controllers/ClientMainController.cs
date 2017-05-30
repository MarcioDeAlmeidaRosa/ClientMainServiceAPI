using ClientMainServiceAPI.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Controller.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClientMainServiceAPI.Controllers
{
    /// <summary>
    /// Controle Responsável por responder as solicitações no escopo de usuário para os clientes que necessitarem deste recurso
    /// </summary>
    [RoutePrefix("api/ClientMain")]
    public class ClientMainController : ApiController, IClientMain
    {
        //como aplicar o swagger
        //http://www.matera.com/br/2016/02/24/swagger-como-gerar-uma-documentacao-interativa-para-api-rest/

        //Rodar o editor do swagger
        //como instalar
        //http://swagger.io/docs/swagger-tools/#swagger-editor-documentation-0

        //http://swagger.io/swagger-editor/


        /// <summary>
        /// Variável para armazenar o model de cliente que sejá injetado ao criar este controle
        /// </summary>
        private IPersonController _model;

        /// <summary>
        /// Metodo construtor que espera ser injetado o model de cliente
        /// </summary>
        /// <param name="model"></param>
        public ClientMainController(IPersonController model)
        {
            _model = model;
        }

        /// <summary>
        /// Busca Cliente (Pessoa física ou jurídica) pelo ID dela
        /// </summary>
        /// <param name="Id">Id do cliente já cadastrado</param>
        /// <returns>Retorna o cliente localizado pelo ID</returns>
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

        /// <summary>
        /// Cria registro de pessoa física
        /// </summary>
        /// <param name="person">Payload de pessoa física</param>
        /// <returns>Retorna a pessoa física cadastrada</returns>
        [AcceptVerbs("POST")]
        [Route("CreatePhysicalClient")]
        public async Task<IHttpActionResult> CreatePhysicalClient([FromBody] PhysicalPerson  person)
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

        /// <summary>
        /// Cria registro de pessoa jurídica
        /// </summary>
        /// <param name="person">Payload de pessoa jurídica</param>
        /// <returns>Retorna a pessoa jurídica cadastrada</returns>
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
