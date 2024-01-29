using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;
using System.Security.Principal;

namespace COMMON;
public static class IdentityExtension
{
        public static string RealName(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).FindFirst("RealName")?.Value ?? string.Empty;
        }
        public static uint PersonId(this IIdentity identity)
        {
           string personIdStr = ((ClaimsIdentity)identity).FindFirst("PersonId")?.Value ?? string.Empty;
           if(uint.TryParse(personIdStr, out uint personId)) return personId;
           return 0;
        }
}