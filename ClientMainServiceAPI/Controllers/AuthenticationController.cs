﻿using ClientMainServiceAPI.Contracts;
using ClientMainServiceAPI.Controller.Contracts;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Domain.Model;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ClientMainServiceAPI.Controllers
{
    /// <summary>
    /// Controle responsável pelos métodos de autenticação
    /// </summary>
    [RoutePrefix("api/Authentication")]
    public class AuthenticationController : ApiController, IAuthentication
    {
        /// <summary>
        /// Variável para armazenar o model de autenticação que sejá injetado ao criar este controle
        /// </summary>
        private IAuthenticationController _model;

        /// <summary>
        /// Metodo construtor que espera ser injetado o model de autenticação
        /// </summary>
        /// <param name="model"></param>
        public AuthenticationController(IAuthenticationController model)
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
                _model.Register(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message, this);
            }
        }

        /// <summary>
        /// Metodo responsável por registrar o usário que faz login por um aplicativo externo
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("LoginExternalAuthentication")]
        public async Task<IHttpActionResult> LoginExternalAuthentication([FromBody] User user)
        {
            ResultAuthentication retorno = null;
            try
            {
                retorno = _model.LoginExternalAuthentication(user);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message, this);
            }
            return Ok(retorno);
        }

        /// <summary>
        /// Metodo responsável por vincular o e-mail de registro com o usuário registrado pelo aplicativo externo
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("LinkExternalAuthentication")]
        public async Task<IHttpActionResult> LinkExternalAuthentication([FromBody] LinkUser user)
        {
            ResultAuthentication retorno = null;
            try
            {
                retorno = _model.LinkExternalAuthentication(user);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message, this);
            }
            return Ok(retorno);
        }

        [AcceptVerbs("POST")]
        [Route("Login")]
        public async Task<IHttpActionResult> Login([FromBody] User user)
        {
            ResultAuthentication retorno = null;
            try
            {
                retorno = _model.Login(user);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message, this);
            }
            return Ok(retorno);
        }
    }
}
