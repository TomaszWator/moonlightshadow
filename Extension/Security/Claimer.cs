using System.Collections.Generic;
using System.Security.Claims;

namespace Extension.Security
{
    public static class Claimer
    {
        public static ClaimsPrincipal CreateClaimPrincipal(string claimTypes, string value, string role = "user")
        {
            var licenseClaims = new List<Claim>()
            {
                new Claim(claimTypes, value),
                new Claim(ClaimTypes.Role, role),
            };

            var userIdentity = new ClaimsIdentity(licenseClaims, "User identity");

            return new ClaimsPrincipal(userIdentity);
        }
    }
}