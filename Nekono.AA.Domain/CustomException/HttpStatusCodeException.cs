using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Nekono.AA.Domain.CustomException
{
    public class HttpStatusCodeException : System.Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusCodeException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
