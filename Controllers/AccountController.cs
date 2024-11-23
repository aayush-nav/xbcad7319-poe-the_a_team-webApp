using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Services;
using XBCAD7319_SparkLine_HR_WebApp.Token;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly FirebaseAuthService _firebaseAuthService;

        public AccountController()
        {
            _firebaseAuthService = new FirebaseAuthService();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            // Hash the provided password
            var hashedPassword = HashPassword(password);

            // Validate credentials with the hashed password
            var isValidAdmin = await _firebaseAuthService.ValidateAdminCredentials(username, hashedPassword);

            if (isValidAdmin)
            {
                // Generate token or set session
                var token = TokenHelper.GenerateToken(username);
                Response.Cookies.Append("jwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Path = "/",
                    Expires = DateTime.Now.AddHours(1) // Expiration time
                });

                System.Diagnostics.Debug.WriteLine($"Generated Token: {token}"); // Log token - REMOVE
                return Json(new { success = true, message = "Login Successful" });
            }

            return Json(new { success = false, message = "Invalid credentials" });
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }


        [HttpGet]
        [Route("Account/Logout")]
        public ActionResult Logout()
        {
            Debug.WriteLine("Logout triggered"); // Should hit here
                                                 // Clear cookie server-side
            Response.Cookies.Append("jwtToken", "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                Path = "/", // Match original path
                HttpOnly = true
            });

            Response.Cookies.Delete("jwtToken", new CookieOptions
            {
                Path = "/"
            });

            // Redirect to login page
            return RedirectToAction("Login", "Account");
        }



      
    }
}
