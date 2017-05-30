using System;

namespace ClientMainServiceAPI.Domain
{
    public class PhysicalPerson : Person
    {
        public DateTime Birth { get; set; }
        public int Age { get; set; }
        public PhysicalDocuments Document { get; set; }
    }
}
