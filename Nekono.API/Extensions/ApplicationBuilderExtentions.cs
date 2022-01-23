using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nekono.AA.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Nekono.API.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app,
                                                    ILogger logger,
                                                    bool isProdEnvironment)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is UnauthorizedAccessException)
                        {
                            await GetExceptionDetails(context,
                                                      Convert.ToInt32(HttpStatusCode.Unauthorized),
                                                      $"{contextFeature.Error.Message} - " +
                                                      $"Invalid Credentials passed to Active Directory",
                                                      contextFeature.Error.StackTrace,
                                                      isProdEnvironment);
                        }
                        else
                        {
                            await GetExceptionDetails(context,
                                                      Convert.ToInt32(HttpStatusCode.InternalServerError),
                                                      $"{contextFeature.Error.Message}",
                                                      contextFeature.Error.StackTrace,
                                                      isProdEnvironment);
                        }

                        logger.LogError(
                        LoggingEvents.ServerError,
                        $"Error Message: {contextFeature.Error.Message} exception thrown" +
                        $"\n Stack Trace: {contextFeature.Error.StackTrace}");
                    }
                });
            });
        }

        public async static Task GetExceptionDetails(
                           HttpContext context,
                           int statusCode,
                           string message,
                           string stackTrace,
                           bool isProdEnvironment)
        {
            BaseExceptionDetails exceptionDetails = new BaseExceptionDetails();

            if (isProdEnvironment)
            {
                exceptionDetails = new BaseExceptionDetails()
                {
                    StatusCode = statusCode,
                    ErrorMessage = message
                };
            }
            else
            {
                exceptionDetails = new DevExceptionDetails()
                {
                    StatusCode = statusCode,
                    ErrorMessage = message,
                    Stacktrace = stackTrace
                };
            }

            await context.Response.WriteAsync(exceptionDetails.ToString());
        }

        public class LoggingEvents
        {
            public const int GetItem = 1000;
            public const int Diagnostics = 2000;
            public const int Validates = 3000;
            public const int ItemNotFound = 4000;
            public const int ServerError = 5000;
        }
    }
}
