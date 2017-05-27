namespace ClientMainServiceAPI.Domain
{
    public class LegalPerson : Person
    {
        public string SocialReason { get; set; }
        public LegalDocuments Document { get; set; }
    }
}
