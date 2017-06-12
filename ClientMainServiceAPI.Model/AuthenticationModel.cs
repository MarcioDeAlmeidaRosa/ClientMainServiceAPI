using System;
using ClientMainServiceAPI.Model.Contracts;
using ClientMainServiceAPI.Model.DB;
using MongoDB.Driver;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Tools;
using System.Configuration;
using ClientMainServiceAPI.Domain.Model;
using System.Collections.Generic;

namespace ClientMainServiceAPI.Model
{
    public class AuthenticationModel : DBFactory<User>, IAuthenticationModel
    {
        private Int32 timeOutSessionLogin = 0;
        private ResultAuthentication resultAut;

        public AuthenticationModel() : base("client-api", "Users")
        {
            timeOutSessionLogin = Convert.ToInt32(ConfigurationManager.AppSettings["time-out-session-login"]);
        }

        public void Register(User entity)
        {
            if (entity == null)
                throw new Exception("Usuário não informado");

            var userCheck = FindByEmail(entity.Email);
            //Checar situação do usuário
            if (userCheck != null)
            {
                if (userCheck.Blocked)
                    throw new Exception("Usuário já cadastrado e bloqueado");

                if (userCheck.WaitingConfirmation)
                    throw new Exception("Usuário já cadastrado e aguardando confirmação por e-mail");

                throw new Exception("Usuário já cadastrado");
            }

            resultAut = new ResultAuthentication
            {
                Token = Guid.NewGuid().ToString()
            };

            //Marca como aguardando confirmação de e-mail
            entity.WaitingConfirmation = true;

            resultAut.User = base.Create(entity);

            try
            {
                Email.SendEmail(entity.Email,
                    entity.Email,
                    "Confirmar registro.",
                    string.Format("{0}{1}/{2}", ConfigurationManager.AppSettings["url-confirm-email"], entity.Id, resultAut.Token),
                    entity.Aplication);
            }
            catch (Exception ex)
            {
                //Remove o registro para o usuário tentar criar de novo.
                DeleteById(Convert.ToString(resultAut.User.Id));
                throw ex;
            }

            //Cria registro de sessão do usuário
            new UserConnectedModel().Create(new UserConnected
            {
                Token = resultAut.Token,
                User = resultAut.User.Id.ToString(),
                LastCall = DateTime.UtcNow,
                Valid = DateTime.UtcNow.AddMinutes(timeOutSessionLogin)
            });
        }

        public void ConfirmEmail(string id, string token)
        {
            var user = GetById(id);

            if (user == null)
                throw new Exception("Usuário não registrado");

            if (!user.WaitingConfirmation)
                throw new Exception("E-mail já confirmado");

            if (user.Blocked)
                throw new Exception("E-mail esta bloqueado");

            var conectado = new UserConnectedModel();

            var tokenValido = conectado.FindByToken(token);

            if (tokenValido == null)
                throw new Exception("Token inválido");

            if (tokenValido.Valid < DateTime.UtcNow)
            {
                //Remove o usuário expirado da sessão
                conectado.DeleteById(tokenValido.Id.ToString());
                //Deleta o registro do usuário para ser feito um novo registro
                DeleteById(id);
                throw new Exception("Token expirado, solicitar registro novamente");
            }

            user.WaitingConfirmation = false;
            var replaceOneResult = _db.GetCollection<User>(CollectionName)
                .ReplaceOne(doc => doc.Id == user.Id, user);

            //Caso exista e-mail registrado no usuário, envia um e-mail informando o registro com sucesso.
            if (!string.IsNullOrWhiteSpace(user.Email))
                SendEmailConfirmationRegister(user.Email, user.Aplication);
        }

        private static void SendEmailConfirmationRegister(string email, int aplication)
        {
            Email.SendEmail(email,
                            email,
                            "Email confirmado com sucesso.",
                            "Seu e-mail acaba de ser confirmado com sucesso no site www.clinicadepilotagem.com<br/>Obrigado por usar nosso serviço",
                            aplication);
        }

        public ResultAuthentication LoginExternalAuthentication(User entity)
        {
            if (entity == null)
                throw new Exception("Usuário não informado");

            if ((entity.Providers == null) || (entity.Providers.Length < 1))
                throw new Exception("Provider do usuário não informado");

            //Primeiro acesso do usuário pelo provider - Marca como Failure para requerir o e-mail
            var retorno = new ResultAuthentication();

            //Localiza o usuário se já existe cadastrados pelos providers
            var user = FindByProvider(entity.Providers[0].Key, entity.Providers[0].Login);

            //Cria registro de sessão do usuário
            if (user != null)
            {
                if (user.Blocked)
                    throw new Exception("Usuário está bloqueado");
                //retorno.StatusLogin = StatusLogin.LockedOut;
                else if (user.WaitingConfirmation)
                    throw new Exception("Por favor, acessar seu e-mail e confirmar o cadastro do usuário");
                //retorno.StatusLogin = StatusLogin.RequiresVerification;
                else
                {
                    //Gera o Token liberando o usuário a acessar o sistema
                    retorno.Token = Guid.NewGuid().ToString();
                    //Cria registro de conexão do usuário para controlar sessão
                    new UserConnectedModel().Create(new UserConnected
                    {
                        Token = retorno.Token,
                        User = user.Id.ToString(),
                        LastCall = DateTime.UtcNow,
                        Valid = DateTime.UtcNow.AddMinutes(timeOutSessionLogin)
                    });
                }
            }

            return retorno;
        }

        public ResultAuthentication LinkExternalAuthentication(LinkUser user)
        {
            if (user == null)
                throw new Exception("Usuário não informado");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new Exception("Email não informado");

            if (user.Provider == null)
                throw new Exception("Provider do usuário não informado");

            var retorno = new ResultAuthentication();

            //Localiza o usuário pelo provider
            var userProvider = FindByProvider(user.Provider.Key, user.Provider.Login);

            if (userProvider == null)
            {
                //Usuário não cadastrado por provider
                userProvider = FindByEmail(user.Email);

                #region [Cria novo usuário]
                if (userProvider == null)
                {
                    //Cria novo usuário
                    userProvider = Create(new User()
                    {
                        Aplication = user.Aplication,
                        Blocked = false,
                        Email = user.Email,
                        Password = string.Empty,
                        UserName = user.Name,
                        WaitingConfirmation = false,
                        Providers = new[] { new Provider
                            {
                                Key = user.Provider.Key ,
                                Login = user.Provider.Login
                            }
                        }
                    });
                }
                #endregion

                #region [Inclui novo provider no usuário já cadastrado]
                else
                {
                    //Inclui novo provider no usuário já cadastrado
                    var providers = new List<Provider>(userProvider.Providers);
                    //Adiciona novo na lista
                    providers.Add(new Provider
                    {
                        Key = user.Provider.Key,
                        Login = user.Provider.Login
                    });
                    //Atualiza objeto
                    userProvider.Providers = providers.ToArray();
                }
                #endregion
            }

            //Atualizar o e-mail no cadastro de usuário
            userProvider.Email = user.Email;
            //Verifica se nome foi informado
            if (string.IsNullOrWhiteSpace(userProvider.UserName))
                userProvider.UserName = user.Name;
            userProvider.Blocked = false;
            userProvider.WaitingConfirmation = false;
            UpdateById(userProvider.Id.ToString(), userProvider);

            //Enviar e-mail de registro
            SendEmailConfirmationRegister(user.Email, user.Aplication);

            var userController = new UserConnectedModel();
            //Verifica se sessão foi criada
            var token = userController.FindByToken(user.Token);
            //Cria ou atualiza a sessão
            if (token == null)
            {
                token = new UserConnected();
                token.LastCall = DateTime.UtcNow;
                token.Valid = DateTime.UtcNow.AddMinutes(timeOutSessionLogin);
                token = userController.Create(token);
            }
            else
            {
                token.LastCall = DateTime.UtcNow;
                token.Valid = DateTime.UtcNow.AddMinutes(timeOutSessionLogin);
                userController.UpdateById(token.Id.ToString(), token);
            }

            retorno.Token = token.Token;
            retorno.User = userProvider;

            return retorno;
        }

        public ResultAuthentication Login(User entity)
        {
            ResultAuthentication retorno = new ResultAuthentication();

            var user = _db.GetCollection<User>(CollectionName)
                .Find(filtro => filtro.Email == entity.Email && filtro.Password == entity.Password)
                .FirstOrDefault();

            if (user == null)
                throw new Exception("Usuário não encontrado");
            else if (user.Blocked)
                throw new Exception("Usuário está bloqueado");
            else if (user.WaitingConfirmation)
                throw new Exception("Por favor, acessar seu e-mail e confirmar o cadastro do usuário");
            else
            {
                //Ajusta / atualiza sessão
                var userController = new UserConnectedModel();
                //Verifica se sessão foi criada
                var token = userController.FindByUserId(user.Id.ToString());
                //Cria ou atualiza a sessão
                if (token == null)
                {
                    token = new UserConnected();
                    token.LastCall = DateTime.UtcNow;
                    token.Valid = DateTime.UtcNow.AddMinutes(timeOutSessionLogin);
                    token = userController.Create(token);
                }
                else
                {
                    token.LastCall = DateTime.UtcNow;
                    token.Valid = DateTime.UtcNow.AddMinutes(timeOutSessionLogin);
                    userController.UpdateById(token.Id.ToString(), token);
                }

                retorno.Token = token.Token;
                retorno.User = user;
            }

            return retorno;
        }

        private User FindByProvider(string providerKey, string providerLogin)
        {
            var filter = Builders<User>.Filter.ElemMatch(x => x.Providers, x => x.Key == providerKey && x.Login == providerLogin);
            return _db.GetCollection<User>(CollectionName).Find(filter).FirstOrDefault(); ;
        }

        private User FindByEmail(string email)
        {
            return _db.GetCollection<User>(CollectionName)
                .Find(filtro => filtro.Email == email)
                .FirstOrDefault();
        }
    }
}
