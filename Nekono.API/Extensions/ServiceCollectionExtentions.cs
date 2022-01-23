using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nekono.AA.Domain.Config;
using Nekono.API.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Nekono.API.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddOauthServerConfig(this IServiceCollection services, EnvironmentConfig config)
        {
            services.Configure<OauthServerConfig>(options =>
            {
                options.ApiName = config.AuthServer.ApiName;
                options.ApiSecret = config.AuthServer.ApiSecret;
                options.BaseAddress = config.AuthServer.BaseAddress;
                options.ClientId = config.AuthServer.ClientId;
                options.Scope = config.AuthServer.Scope;
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                    {
                        options.Authority = config.AuthServer.BaseAddress;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false
                        };
                        options.RequireHttpsMetadata = false;
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(config.AuthServer.ApiName, policyUser =>
                {
                    policyUser.RequireClaim("scope", config.AuthServer.Scope);
                });
            });
        }

        public static void AddNekonoAppConfig(this IServiceCollection services, EnvironmentConfig config)
        {
            services.Configure<NekonoAppConfig>(options =>
            {
                options.DbConn = config.AppConfig.DbConn;
                options.LogsPath = config.AppConfig.LogsPath;
            });
        }

        private static HttpClientHandler TurnOffHttpsValidation()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true
            };
        }
    }
}
