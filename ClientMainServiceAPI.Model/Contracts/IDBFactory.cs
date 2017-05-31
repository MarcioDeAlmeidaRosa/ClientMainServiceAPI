namespace ClientMainServiceAPI.Model.Contracts
{
    public interface IDBFactory<T>
    {
        T GetById(string id);
        T Create(T entity);
        void DeleteById(string id);
        void UpdateById(string id, T entity);
    }
}
