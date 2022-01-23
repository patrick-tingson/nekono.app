using Microsoft.Extensions.Configuration;

namespace Nekono.Web.Config
{
    public class EnvironmentConfig
    {
        public bool HttpsValidationEnabled { get; private set; }
        public AppConfig AppConfig { get; set; }

        public static EnvironmentConfig FromConfig(IConfiguration config)
        {
            return new EnvironmentConfig
            {
                HttpsValidationEnabled = false,
                AppConfig = AppConfig.FromConfig(config)
            };
        }
    }

    public class AppConfig
    {
        public string NekonoAPI { get; set; }
        public string LogsPath { get; set; }

        public static AppConfig FromConfig(IConfiguration config)
        {
            var section = config.GetSection(nameof(AppConfig));

            return new AppConfig()
            {
                NekonoAPI = section[nameof(NekonoAPI)],
                LogsPath = section[nameof(LogsPath)]
            };
        }
    }
}