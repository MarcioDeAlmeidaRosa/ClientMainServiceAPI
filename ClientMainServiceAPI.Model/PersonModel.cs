using System;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Contracts;

namespace ClientMainServiceAPI.Model
{
    public class PersonModel : IPersonModel
    {
        public Person GetById(string id)
        {
            return new PhysicalPerson { _Id = new Guid(), Name = "Marcio de Almeida Rosa", Birth = new DateTime(1983, 11, 04), Age = 33, Document = new PhysicalDocuments { CPF = "456.852.789-8", RG = "85.652.793-9" } };
        }
    }
}
