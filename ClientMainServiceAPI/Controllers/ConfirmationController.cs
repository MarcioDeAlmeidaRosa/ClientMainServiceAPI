using ClientMainServiceAPI.Controller.Contracts;
using System;
using System.Web.Mvc;

namespace ClientMainServiceAPI.Controllers
{
    [RoutePrefix("Confirmation")]
    public class ConfirmationController : System.Web.Mvc.Controller
    {
        private IAutenticationController _model;

        /// <summary>
        /// Metodo construtor que espera ser injetado o model de autenticação
        /// </summary>
        /// <param name="model"></param>
        ///TODO - APLICAR INJEÇÃO AQUI TB
        //public ConfirmationController(IAutenticationController model)
        //{
        //    _model = model;
        //}

            //TODO - REMOVER
        public ConfirmationController()
        {
            _model = new Controller.AutenticationController();
        }

        /// <summary>
        /// Metodo resposnável por confirmar o registro do usuário e liberar o acesso ao sistema
        /// </summary>
        /// <param name="value">Valor da querystring enviado por e-mail para confirmação do usuário</param>
        /// <returns>200 caso sucesso/400 caso erro</returns>
        [HttpGet]
        [Route("ConfirmEmail/{id}/{token}")]
        public ActionResult ConfirmEmail(string id, string token)
        {
            try
            {
                _model.Confirm(id, token);
                ViewBag.Sucesso = "Confirmação realizada com sucesso";
                ViewBag.Erro = "";
            }
            catch(Exception ex)
            {
                ViewBag.Sucesso = "";
                ViewBag.Erro = ex.Message;
            }
            return View();
        }
    }
}