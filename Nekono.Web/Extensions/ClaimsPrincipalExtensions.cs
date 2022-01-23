using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nekono.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetTokenUsername(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));

            return claim != null ? claim.Value : string.Empty;
        }

        public static string GetTokenStringValue(this ClaimsPrincipal principal, string name)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type.Equals(name));

            return claim != null ? claim.Value : string.Empty;
        }

        public static int GetTokenIntValue(this ClaimsPrincipal principal, string name)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type.Equals(name));

            return claim != null ? int.Parse(claim.Value) : 0;
        }

        public static string GetAccessToken(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData));

            return claim != null ? claim.Value : string.Empty;
        }
    }
}
