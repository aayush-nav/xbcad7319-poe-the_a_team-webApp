using Firebase.Database;
using Firebase.Database.Query;

namespace XBCAD7319_SparkLine_HR_WebApp.Services
{
    // Service class for the Authentication service
    // get the data from the data base for us to compare the credentials
    public class FirebaseAuthService
    {
        private readonly FirebaseClient _firebaseClient;

        // initialized the firebase client
        public FirebaseAuthService()
        {
            _firebaseClient = new FirebaseClient("https://mvc-hr-demo-default-rtdb.firebaseio.com/");
        }

        /// <summary>
        /// Method to validated the admin credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
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

    // Model for the user
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
