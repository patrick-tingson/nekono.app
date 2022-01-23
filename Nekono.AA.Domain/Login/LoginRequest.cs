using System;
using System.Collections.Generic;
using System.Text;

namespace Nekono.AA.Domain.Login
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Signature { get; set; }
    }
}
