namespace ClientMainServiceAPI.Domain.Model
{
    public class ResultAuthentication
    {
        public string Token { get; set; }
        public StatusLogin StatusLogin { get; set; }
        public User User { get; set; }
    }
}
