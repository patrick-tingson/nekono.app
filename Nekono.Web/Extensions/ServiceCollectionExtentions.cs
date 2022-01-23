using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nekono.AA.Domain.Config;
using Nekono.Web.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Nekono.Web.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddNekonoAppConfig(this IServiceCollection services, EnvironmentConfig config)
        {
            services.Configure<NekonoAppConfig>(options =>
            {
                options.NekonoAPI = config.AppConfig.NekonoAPI;
                options.LogsPath = config.AppConfig.LogsPath;
            });
        }
    }
}
