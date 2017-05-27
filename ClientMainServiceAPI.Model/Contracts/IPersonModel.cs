using ClientMainServiceAPI.Domain;
using System;

namespace ClientMainServiceAPI.Model.Contracts
{
    public interface IPersonModel
    {
        Person GetById(string Id);
    }
}
