using Microsoft.Extensions.Configuration;

namespace Nekono.API.Config
{
    public class EnvironmentConfig
    {
        public bool HttpsValidationEnabled { get; private set; }
        public AuthServer AuthServer { get; set; }
        public AppConfig AppConfig { get; set; }

        public static EnvironmentConfig FromConfig(IConfiguration config)
        {
            return new EnvironmentConfig
            {
                HttpsValidationEnabled = false,
                AuthServer = AuthServer.FromConfig(config),
                AppConfig = AppConfig.FromConfig(config)
            };
        }
    }

    public class AuthServer
    {
        public string BaseAddress { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }

        public static AuthServer FromConfig(IConfiguration config)
        {
            var section = config.GetSection(nameof(AuthServer));

            return new AuthServer()
            {
                ApiName = section[nameof(ApiName)],
                ApiSecret = section[nameof(ApiSecret)],
                ClientId = section[nameof(ClientId)],
                BaseAddress = section[nameof(BaseAddress)],
                Scope = section[nameof(Scope)]
            };
        }
    }

    public class AppConfig
    {
        public string DbConn { get; set; }
        public string LogsPath { get; set; }

        public static AppConfig FromConfig(IConfiguration config)
        {
            var section = config.GetSection(nameof(AppConfig));

            return new AppConfig()
            {
                DbConn = section[nameof(DbConn)],
                LogsPath = section[nameof(LogsPath)]
            };
        }
    }
}