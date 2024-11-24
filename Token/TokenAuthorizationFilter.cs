using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace XBCAD7319_SparkLine_HR_WebApp.Token
{
    /// <summary>
    /// Custom Action Filter to handle token-based authorization.
    /// Ensures the presence and validity of a JWT token in cookies before executing an action.
    /// </summary>
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Executes before an action method is invoked.
        /// Validates the JWT token from cookies and redirects to the login page if invalid or absent.
        /// </summary>
        /// <param name="filterContext">Context for action execution.</param>
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

            // Proceed to the action execution
            base.OnActionExecuting(filterContext);
        }
    }
}
