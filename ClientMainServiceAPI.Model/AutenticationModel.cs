using System;
using ClientMainServiceAPI.Model.Contracts;
using ClientMainServiceAPI.Model.DB;
using MongoDB.Driver;
using MongoDB.Bson;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Utils;
using System.Configuration;

namespace ClientMainServiceAPI.Model
{
    public class AutenticationModel : DBFactory<User>, IAutenticationModel
    {
        public AutenticationModel() : base("client-api", "Users")
        {

        }

        public override User Create(User entity)
        {
            //Marca como aguardando confirmação de e-mail
            entity.WaitingConfirmation = true;
            var user = base.Create(entity);

            Email.SendEmail(entity.Email, 
                entity.Email, 
                "Confirmar registro.", 
                ConfigurationManager.AppSettings["url-confirm-email"] + entity.Id, 
                entity.Aplication);
            
            return user;
        }

        public void ConfirmEmail(string id)
        {
            //var user = _db.GetCollection<User>(CollectionName)
            //    .Find(filtro => filtro.Id == new ObjectId(id))
            //    .FirstOrDefault();
            //user.WaitingConfirmation = false;

            //var filter = Builders<BsonDocument>.Filter.Eq("id", new ObjectId(id));
            //var update = Builders<BsonDocument>.Update.Set("AguardandoConfirmacao", "false");

            //_db.GetCollection<User>(CollectionName).UpdateOne(filter, update);

        }
    }
}
