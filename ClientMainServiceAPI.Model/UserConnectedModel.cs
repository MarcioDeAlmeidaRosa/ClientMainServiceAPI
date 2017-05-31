using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Contracts;
using ClientMainServiceAPI.Model.DB;
using MongoDB.Driver;

namespace ClientMainServiceAPI.Model
{
    public class UserConnectedModel : DBFactory<UserConnected>, IUserConnectedModel
    {
        public UserConnectedModel() : base("client-api", "UserConnected")
        {
        }

        public UserConnected FindByToken(string token)
        {
            return _db.GetCollection<UserConnected>(CollectionName)
                .Find(filtro => filtro.Token == token)
                .FirstOrDefault();
        }
    }
}
