using System;

namespace ClientMainServiceAPI.Domain
{
    public abstract class Person
    {
        public Guid _Id { get; set; }
        public string Name { get; set; }
    }
}
