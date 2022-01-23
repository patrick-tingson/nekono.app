using Nekono.AA.Data;
using Nekono.AA.Domain.Login;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Nekono.AA.Business
{
    public class LoginLogic : ILoginLogic
    {
        private readonly ILoginServices loginServices;

        public LoginLogic(ILoginServices loginServices)
        {
            this.loginServices = loginServices ??
              throw new ArgumentNullException(nameof(loginServices));
        }

        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse();
            var validCredential = await loginServices.Authenticate(loginRequest.Username, loginRequest.Password);

            if(validCredential)
            {
                loginResponse = await loginServices.GetToken(loginRequest.Username, loginRequest.Password);
            }
            else
            {
                loginResponse.Error = "Invalid username or password";
            }

            return loginResponse;
        }
    }
}
