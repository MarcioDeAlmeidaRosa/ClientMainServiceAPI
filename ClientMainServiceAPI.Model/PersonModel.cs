using System;
using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.Contracts;

namespace ClientMainServiceAPI.Model
{
    public class PersonModel : IPersonModel
    {
        public Person GetById(string id)
        {
            //return new PhysicalPerson
            //{
            //    //TODO - COLOCAR PESQUISA
            //    _Id = Guid.NewGuid(),
            //    Name = "Marcio de Almeida Rosa",
            //    Birth = new DateTime(1983, 11, 04),
            //    Age = 33,
            //    Document = new PhysicalDocuments
            //    {
            //        CPF = "456.852.789-8",
            //        RG = "85.652.793-9"
            //    }
            //};
            return new LegalPerson
            {
                //TODO - COLOCAR PESQUISA
                _Id = Guid.NewGuid(),
                Name = "MARC SUPORTE TÉCNICO",
                Document = new LegalDocuments
                {
                    CNPJ = "54.564.564/0001-65"
                }
            };
        }

        public Person CreatePerson(Person person)
        {
            person._Id = Guid.NewGuid();
            //TODO - COLOCAR CREATE
            return person;
        }
    }
}
