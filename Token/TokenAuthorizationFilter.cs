using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace XBCAD7319_SparkLine_HR_WebApp.Token
{
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Retrieve the token from the cookie
            var token = filterContext.HttpContext.Request.Cookies["jwtToken"];

            // Validate the token
            if (string.IsNullOrEmpty(token) || TokenHelper.ValidateToken(token) == null)
            {
                // If invalid, redirect to login
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
