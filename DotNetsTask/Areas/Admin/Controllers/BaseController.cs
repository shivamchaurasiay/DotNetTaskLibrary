
using DotNetsTask.Data.Models;
using DotNetsTasks.Core;
using DotNetsTasks.Models.Others;
using DotNetsTasks.Web.Code.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;


namespace DotNetsTask.Areas.Admin.Controllers

{
    public class BaseController : Controller
    {
        #region "Public Properties"

        public CustomPrincipal CurrentUser => new CustomPrincipal(HttpContext.User);
        public ClaimsPrincipal LoggedinUser => HttpContext.User;

        #endregion

        #region "Authentication"

        public async Task CreateAuthenticationTicket(User user)
        {
            if (user != null)
            {
                

                var claims = new List<Claim>{
                        new Claim(ClaimTypes.Email, user.Email??""),
                        new Claim(ClaimTypes.Name,user.Name??""),
                        new Claim(nameof(user.RoleId), Convert.ToString(user.RoleId)),
                        new Claim(ClaimTypes.PrimarySid,Convert.ToString(user.UserId))
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
        }

        public async Task RemoveAuthentication()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion



        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    // Check if the current request is already targeting the login page
        //    if (!CurrentUser.IsAuthenticated && !context.HttpContext.Request.Path.Equals("/Admin", StringComparison.OrdinalIgnoreCase))
        //    {
        //        context.Result = new RedirectResult("/Admin");

        //        return;
        //    }

        //    base.OnActionExecuted(context);
        //}




        #region "Notificatons"
        private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView)
        {
            Notifications model = new Notifications
            {
                Heading = title,
                Message = message,
                Type = messageType
            };

            if (isCurrentView)
                this.ViewData.AddOrReplace("NotificationModel", model);
            else
            {
                TempData["NotificationModel"] = JsonConvert.SerializeObject(model);
                TempData.Keep("NotificationModel");
            }
        }

        protected void ShowErrorMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Danger, isCurrentView);
        }

        protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Success, isCurrentView);
        }

        protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Warning, isCurrentView);
        }

        protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Info, isCurrentView);
        }
        #endregion

        public IActionResult NewtonSoftJsonResult(object data)
        {
            return Json(data);
        }
    }
}
