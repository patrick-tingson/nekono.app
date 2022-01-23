using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nekono.AA.Domain.Config;
using Nekono.AA.Domain.CustomException;
using Nekono.AA.Domain.Model;
using Nekono.API.Logging;

namespace Nekono.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class ErrorController : ControllerBase
    {
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public ErrorController(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        [Route("error")]
        public async Task<ActionResult<ExceptionDetails>> Error([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var exceptionDetails = new ExceptionDetails();
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = 500;

            if(context != null)
            {
                if(context.Error is HttpStatusCodeException httpStatusCodeException)
                {
                    statusCode = (int)httpStatusCodeException.Status;
                    exceptionDetails.StatusCode = statusCode;
                    exceptionDetails.ErrorMessage = context.Error.Message;
                    exceptionDetails.Stacktrace = context.Error.StackTrace;
                }
                else
                {
                    exceptionDetails.StatusCode = statusCode;
                    exceptionDetails.ErrorMessage = context.Error.Message;
                    exceptionDetails.Stacktrace = context.Error.StackTrace;
                }

                Logs.Write($"{exceptionDetails.StatusCode}|{exceptionDetails.ErrorMessage}|{exceptionDetails.Stacktrace}", 
                    Logs.LogStatus.Error, nekonoAppConfig.Value.LogsPath);
            }

            Response.StatusCode = statusCode;

            return exceptionDetails;
        }
    }
}
