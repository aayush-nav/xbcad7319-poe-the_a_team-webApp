using Microsoft.AspNetCore.Mvc;
using XBCAD7319_SparkLine_HR_WebApp.Models;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate user (Firebase auth or any logic here)


                // If successful, redirect to the desired page
                return RedirectToAction("MainDashboard", "Home");
            }

            // If authentication fails, reload the login page with an error message
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            // Forgot password logic
            return View();
        }

        public IActionResult Logout()
        {
            // Perform any necessary logout operations, like clearing session or authentication cookies.
            //HttpContext.Session.Clear(); // Optional: clear session data if used.

            // Redirect to the Login action or view after logging out.
            return RedirectToAction("Login", "Account");
        }
    }
}
