using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nekono.AA.Business;
using Nekono.AA.Domain.CustomException;
using Nekono.AA.Domain.Login;
using Nekono.AA.Domain.Utility;

namespace Nekono.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly ILoginLogic loginLogic;

        public LoginController(ILogger<LoginController> logger, ILoginLogic loginLogic)
        {
            this.logger = logger ??
              throw new ArgumentNullException(nameof(logger));

            this.loginLogic = loginLogic ??
              throw new ArgumentNullException(nameof(loginLogic));
        }

        [HttpPost]
        [Route("v1/[controller]")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var client = new HttpClient();
            var verifySignature = Signature.Create(loginRequest.Username, loginRequest.Password);

            //if(loginRequest.Signature != verifySignature)
            //{
            //    return StatusCode(400, "Invalid signature.");
            //}

            return StatusCode(200, await loginLogic.Authenticate(loginRequest));
        }
    }
}
