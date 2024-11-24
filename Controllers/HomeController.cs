using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Token;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    /// <summary>
    ///  Home controller - shows the view for the main dashboard
    ///  makes use of the TokenAuthorizationFilter for all the methods to block the routes
    /// </summary>
    public class HomeController : Controller
    {
        // <summary>
        /// Returns the Main Dashboard View
        /// </summary>
        [TokenAuthorizationFilter]
        public IActionResult MainDashboard()
        {
            return View();
        }
    }
}
