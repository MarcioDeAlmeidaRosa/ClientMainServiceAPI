using ClientMainServiceAPI.Domain;
using ClientMainServiceAPI.Model.DB;

namespace ClientMainServiceAPI.Model
{
    public class PersonModel : DBFactory<Person>
    {
        //TODO - TEPORÁRIO
        private LegalPersonModel legalPersonModel;
        private PhysicalPersonModel physicalPersonModel;

        //TODO - TEPORÁRIO
        //public PersonModel(LegalPersonModel legalPersonModel , PhysicalPersonModel physicalPersonModel) : base("client-api", "Persons")
        //{
        //    this.legalPersonModel = legalPersonModel;
        //    this.physicalPersonModel = physicalPersonModel;
        //}

        public PersonModel() : base("client-api", "Persons")
        {
            legalPersonModel = new LegalPersonModel();
            physicalPersonModel = new PhysicalPersonModel();
        }

        //TODO - TEMPORÁRIO
        public override Person GetById(string id)
        {
            //TODO - TEMPORÁRIO
            Person retorno = null;
            try
            {
                retorno = legalPersonModel.GetById(id);
            }
            catch
            {
                retorno = physicalPersonModel.GetById(id);
            }
            return retorno;
        }
    }
}
