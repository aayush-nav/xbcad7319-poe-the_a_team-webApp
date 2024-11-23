using Firebase.Database;
using Firebase.Database.Query;

namespace XBCAD7319_SparkLine_HR_WebApp.Services
{
    public class FirebaseAuthService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseAuthService()
        {
            _firebaseClient = new FirebaseClient("https://mvc-hr-demo-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> ValidateAdminCredentials(string username, string password)
        {
            var admins = await _firebaseClient
                .Child("users")
                .OrderByKey()
                .OnceAsync<User>();

            var admin = admins.FirstOrDefault(u => u.Object.Username == username && u.Object.Password == password);
            return admin != null;
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
