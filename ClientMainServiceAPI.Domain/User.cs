﻿namespace ClientMainServiceAPI.Domain
{
    public class User : DomainBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool WaitingConfirmation { get; set; }
        public int Aplication { get; set; }
    }
}
