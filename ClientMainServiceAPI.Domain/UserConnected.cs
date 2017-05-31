using System;

namespace ClientMainServiceAPI.Domain
{
    public class UserConnected : DomainBase
    {
        public string Token { get; set; }
        public string User { get; set; }
        public DateTime LastCall { get; set; }
        public DateTime Valid { get; set; }
    }
}
