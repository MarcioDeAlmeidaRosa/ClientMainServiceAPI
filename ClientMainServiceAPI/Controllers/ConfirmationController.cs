using System.Web.Mvc;

namespace ClientMainServiceAPI.Controllers
{
    [RoutePrefix("Confirmation")]
    public class ConfirmationController : System.Web.Mvc.Controller
    {
        /// <summary>
        /// Metodo resposnável por confirmar o registro do usuário e liberar o acesso ao sistema
        /// </summary>
        /// <param name="value">Valor da querystring enviado por e-mail para confirmação do usuário</param>
        /// <returns>200 caso sucesso/400 caso erro</returns>
        [HttpGet]
        [Route("Confirmar/{value}")]
        public ActionResult Confirm(string value)
        {
            return View();
        }
    }
}