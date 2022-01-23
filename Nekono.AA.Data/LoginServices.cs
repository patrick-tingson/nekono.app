using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nekono.AA.Domain.Login;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Nekono.AA.Domain.Config;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Nekono.AA.Data
{
    public class LoginServices : ILoginServices
    {
        private readonly IOptions<OauthServerConfig> oauthServerConfig;
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public LoginServices(IOptions<OauthServerConfig> oauthServerConfig,
            IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.oauthServerConfig = oauthServerConfig ??
              throw new ArgumentNullException(nameof(oauthServerConfig));

            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            bool result = false;

            using (SqlConnection SQLConnect = new SqlConnection(nekonoAppConfig.Value.DbConn))
            {
                SQLConnect.Open();

                SqlCommand SQLCommand = new SqlCommand(StoredProcedures.LoginSP, SQLConnect);

                SQLCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SQLCommand.Parameters.AddWithValue("@username", username);
                SQLCommand.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = SQLCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }

                reader.Close();
            }

            return result;
        }

        public async Task<LoginResponse> GetToken(string userName, string password)
        {
            var client = new HttpClient();

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{oauthServerConfig.Value.BaseAddress}connect/token",
                ClientId = oauthServerConfig.Value.ClientId,
                ClientSecret = oauthServerConfig.Value.ApiSecret,
                GrantType = "password",
                UserName = userName,
                Password = password,
                Scope = oauthServerConfig.Value.Scope
            });

            if(tokenResponse.IsError)
            {
                throw new ApplicationException($"GT00: {tokenResponse.Error}");
            }

            return JsonConvert.DeserializeObject<LoginResponse>(tokenResponse.Json.ToString());
        }

    }
}
