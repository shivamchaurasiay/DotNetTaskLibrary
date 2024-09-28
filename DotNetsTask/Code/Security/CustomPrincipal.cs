using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json.Serialization;
using DotNetsTasks.Web.LIBS;


namespace DotNetsTasks.Web.Code.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public bool IsAuthenticated { get; private set; }
        public int UserID { get; set; }
        public string UserName { get; private set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleID { get; set; }


        [JsonIgnore]
        public IIdentity Identity { get; private set; }


        private readonly ClaimsPrincipal claimsPrincipal;

        //public CustomPrincipal(ClaimsPrincipal principal)
        //{
        //    claimsPrincipal = principal;
        //    this.IsAuthenticated = claimsPrincipal.Identity.IsAuthenticated;
        //    if (this.IsAuthenticated)
        //    {
        //        this.Identity = new GenericIdentity(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.UserName))?.Value);

        //        this.UserID = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.PrimarySid)?.Value);
        //        this.UserName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.UserName))?.Value;
        //        this.Email = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value;
        //        this.RoleID = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.RoleID))?.Value);


        //    }
        //}


        public CustomPrincipal(ClaimsPrincipal principal)
        {
            claimsPrincipal = principal;
            this.IsAuthenticated = claimsPrincipal.Identity.IsAuthenticated;
            if (this.IsAuthenticated)
            {
                this.Identity = claimsPrincipal.Identity;

                // Extracting claims
                var userNameClaim = claimsPrincipal.FindFirst(ClaimTypes.Name);
                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.PrimarySid);
                var emailClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);
                
                var roleId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;


                // Setting properties
                this.UserID = int.Parse(userIdClaim?.Value);
                this.UserName = userNameClaim?.Value;
                this.Email = emailClaim?.Value;
                this.RoleID = int.Parse(roleId);
            }
        }


        private void UpdateClaim(string key, string value)
        {
            var claims = claimsPrincipal.Claims.ToList();
            if (claims.Any())
            {
                var pmClaim = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == key);
                if (pmClaim != null)
                {
                    claims.Remove(pmClaim);
                    claims.Add(new Claim(key, value));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            ContextProvider.HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties
                 ).Wait();
        }
        public bool IsInRole(int roleID)
        {
            return RoleID == roleID;
        }


        public bool IsInRole(string roleID)
        {
            return RoleID == Convert.ToInt32(roleID);
        }






    }
}
