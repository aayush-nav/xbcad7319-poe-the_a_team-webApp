using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Token;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class HomeController : Controller
    {
        [TokenAuthorizationFilter]
        public IActionResult MainDashboard()
        {
            return View();
        }
    }
}
