using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nekono.AA.Domain.Config;
using Nekono.AA.Domain.Login;
using Nekono.AA.Domain.Utility;
using Nekono.Web.Config;

namespace Nekono.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string API_LOGIN = "v1/login";
        private readonly IOptions<NekonoAppConfig> nekonoAppConfig;

        public LoginController(IOptions<NekonoAppConfig> nekonoAppConfig)
        {
            this.nekonoAppConfig = nekonoAppConfig ??
              throw new ArgumentNullException(nameof(nekonoAppConfig));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginRequest loginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    loginRequest.Password = Signature.SHA1(loginRequest.Password);
                    loginRequest.Signature = Signature.Create(loginRequest.Username, loginRequest.Password);

                    var authenticateResult = await httpClient.PostAsync($"{nekonoAppConfig.Value.NekonoAPI}{API_LOGIN}", loginRequest, JsonConfig.PostFormatter());

                    if(authenticateResult.IsSuccessStatusCode)
                    {
                        using (var content = authenticateResult.Content)
                        {
                            var result = content.ReadAsAsync<LoginResponse>().Result ??
                                           throw new ArgumentNullException(nameof(loginRequest));

                            if(result.AccessToken != null)
                            {
                                var identity = new ClaimsIdentity(new[] {
                                    new Claim(ClaimTypes.Name, loginRequest.Username),
                                    new Claim(ClaimTypes.UserData, result.AccessToken)
                                }, CookieAuthenticationDefaults.AuthenticationScheme);

                                var principal = new ClaimsPrincipal(identity);

                                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                                return Redirect("Home");
                            }
                            else
                            {
                                ViewBag.Message = result.Error;
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Failed to login. Please contact system admin.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return View();
        }
    }
}
