namespace ClientMainServiceAPI.Domain
{
    public abstract class Person: DomainBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
