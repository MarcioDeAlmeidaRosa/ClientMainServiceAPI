using MongoDB.Bson;

namespace ClientMainServiceAPI.Domain
{
    public abstract class DomainBase
    {
        public ObjectId Id { get; set; }
    }
}
