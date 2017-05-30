using ClientMainServiceAPI.Contracts;
using ClientMainServiceAPI.Controller.Contracts;
using ClientMainServiceAPI.Domain;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClientMainServiceAPI.Controllers
{
    /// <summary>
    /// Controle responsável pelos métodos de autenticação
    /// </summary>
    [RoutePrefix("api/Autentication")]
    public class AutenticationController : ApiController, IAutentication
    {
        /// <summary>
        /// Variável para armazenar o model de autenticação que sejá injetado ao criar este controle
        /// </summary>
        private IAutenticationController _model;

        /// <summary>
        /// Metodo construtor que espera ser injetado o model de autenticação
        /// </summary>
        /// <param name="model"></param>
        public AutenticationController(IAutenticationController model)
        {
            _model = model;
        }

        /// <summary>
        /// Metodo responsável por registrar o usuário na aplicação
        /// </summary>
        /// <param name="user">Usuário à ser cadastrado no sistema</param>
        /// <returns>200 caso sucesso/400 caso erro</returns>
        [AcceptVerbs("POST")]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody] User user)
        {
            try
            {   
                _model.Create(user);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
