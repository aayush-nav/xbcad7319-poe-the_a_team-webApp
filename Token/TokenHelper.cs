using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XBCAD7319_SparkLine_HR_WebApp.Token
{
    /// <summary>
    /// Helper class for generating and validating JWT tokens.
    /// Used for managing authentication and securing user sessions.
    /// </summary>
    public class TokenHelper
    {
        private const string SecretKey = "thisIsmySecretKeyAndWeHaveBuiltThisAppForSparkLineHR"; // Replace with your secret key
        private const string Issuer = "SparklineHR";
        private const string Audience = "SparkLineEmployees";

        /// <summary>
        /// Generates a JWT token for a given username.
        /// </summary>
        /// <param name="username">The username to include in the token.</param>
        /// <returns>A signed JWT token as a string.</returns>
        public static string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin") // Optional: Add roles or other claims
        };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates a JWT token and returns the associated ClaimsPrincipal if valid.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>
        /// A ClaimsPrincipal object if the token is valid; otherwise, null.
        /// </returns>
        public static ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,  // You can enable validation as per your setup
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };

                // Validate the token and return the ClaimsPrincipal
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                return principal;
            }
            catch (Exception)
            {
                // Return null if validation fails
                return null;
            }
        }
    }
}
