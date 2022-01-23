using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nekono.AA.Business;
using Nekono.AA.Data;
using Nekono.API.AutoMapper;
using Nekono.API.Config;
using Nekono.API.Extentions;

namespace Nekono.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly EnvironmentConfig envConfig;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            envConfig = EnvironmentConfig.FromConfig(configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddControllers().AddNewtonsoftJson(option => {
                option.UseCamelCasing(true);
            });

            services.AddOauthServerConfig(envConfig);

            services.AddNekonoAppConfig(envConfig);

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ILoginLogic, LoginLogic>();
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IItemLogic, ItemLogic>();
            services.AddScoped<IItemServices, ItemServices>();
            services.AddScoped<IInventoryLogic, InventoryLogic>();
            services.AddScoped<IInventoryServices, InventoryServices>();
            services.AddScoped<ICollectionReceiptLogic, CollectionReceiptLogic>();
            services.AddScoped<ICollectionReceiptsServices, CollectionReceiptServices>();

            services.AddSwaggerGen(c => 
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nekono API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.ConfigureExceptionHandler(logger, false);
            app.UseExceptionHandler($"/error");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nekono API v1");
            });
        }
    }
}
