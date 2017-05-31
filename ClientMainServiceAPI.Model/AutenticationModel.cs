using System;
using ClientMainServiceAPI.Model.Contracts;
using ClientMainServiceAPI.Model.DB;
using MongoDB.Driver;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Tools;
using System.Configuration;
using ClientMainServiceAPI.Domain.Model;

namespace ClientMainServiceAPI.Model
{
    public class AutenticationModel : DBFactory<User>, IAutenticationModel
    {
        public AutenticationModel() : base("client-api", "Users")
        {

        }

        public void Register(User entity)
        {
            if (entity == null)
                throw new Exception("Usuário não informado");

            ResultAutentication retorno = new ResultAutentication
            {
                Token = Guid.NewGuid().ToString(),
                StatusLogin = StatusLogin.Success
            };

            //Marca como aguardando confirmação de e-mail
            entity.WaitingConfirmation = true;

            retorno.User = base.Create(entity);

            Email.SendEmail(entity.Email,
                entity.Email,
                "Confirmar registro.",
                string.Format("{0}{1}/{2}", ConfigurationManager.AppSettings["url-confirm-email"], entity.Id, retorno.Token),
                entity.Aplication);

            //Cria registro de sessão do usuário
            new UserConnectedModel().Create(new UserConnected
            {
                Token = retorno.Token,
                User = retorno.User.Id.ToString(),
                LastCall = DateTime.UtcNow,
                Valid = DateTime.UtcNow.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["time-out-session-login"]))
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

        public ResultAutentication LoginExternalAuthentication(User entity)
        {
            if (entity == null)
                throw new Exception("Usuário não informado");

            if ((entity.Providers == null) || (entity.Providers.Length < 1))
                throw new Exception("Provider do usuário não informado");

            ResultAutentication retorno = new ResultAutentication
            {
                Token = Guid.NewGuid().ToString(),
                StatusLogin = StatusLogin.Success
            };
            //Localiza registro do usuário já cadastrado
            var user = FindByProvider(entity.Providers[0].Key, entity.Providers[0].Login);

            if (user == null)
            {
                //Primeiro acesso do usuário pelo provider
                retorno.User = base.Create(entity);
                retorno.StatusLogin = StatusLogin.NewUser;
            }
            //Cria registro de sessão do usuário
            new UserConnectedModel().Create(new UserConnected
            {
                Token = retorno.Token,
                User = retorno.User.Id.ToString(),
                LastCall = DateTime.Now,
                Valid = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["time-out-session-login"]))
            });

            return retorno;
        }

        private User FindByProvider(string providerKey, string providerLogin)
        {
            var filter = Builders<User>.Filter.ElemMatch(x => x.Providers, x => x.Key == providerKey && x.Login == providerLogin);
            return _db.GetCollection<User>(CollectionName).Find(filter).FirstOrDefault(); ;
        }

        public ResultAutentication LinkExternalAuthentication(LinkUser user)
        {
            if (user == null)
                throw new Exception("Usuário não informado");

            if (user.Provider == null)
                throw new Exception("Provider do usuário não informado");

            ResultAutentication retorno = new ResultAutentication
            {
                //Token = Guid.NewGuid().ToString(),
                StatusLogin = StatusLogin.Success
            };

            //Localiza registro do usuário já cadastrado
            var userProvider = FindByProvider(user.Provider.Key, user.Provider.Login);

            //Não encontrado o pré cadastro do usuário
            if (userProvider == null)
                throw new Exception("Usuário não autenticado");

            //TODO - JUNTAR USUÁRIOS COM O MESMO E-MAIL, ADICIONANDO A LISTA DE PROVIDERS
            //Atualizar o e-mail no cadastro de usuário
            userProvider.Email = user.Email;
            userProvider.Blocked = false;
            userProvider.WaitingConfirmation = false;
            UpdateById(userProvider.Id.ToString(), userProvider);

            //Enviar e-mail de registro
            SendEmailConfirmationRegister(user.Email, user.Aplication);

            //TODO - ATUALIZAR SESSÃO E DEVOLVER O TOKE NA RESPOSTA

            retorno.User = userProvider;

            return retorno;
        }
    }
}
