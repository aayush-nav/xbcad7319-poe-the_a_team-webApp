using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XBCAD7319_SparkLine_HR_WebApp.Token
{
    public class TokenHelper
    {
        private const string SecretKey = "thisIsmySecretKeyAndWeHaveBuiltThisAppForSparkLineHR"; // Replace with your secret key
        private const string Issuer = "SparklineHR";
        private const string Audience = "SparkLineEmployees";

        // Method to generate a token
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

        // Method to validate a token
        //    public static ClaimsPrincipal ValidateToken(string token)
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.UTF8.GetBytes(SecretKey);

        //        try
        //        {
        //            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                ValidIssuer = Issuer,
        //                ValidAudience = Audience,
        //                IssuerSigningKey = new SymmetricSecurityKey(key)
        //            }, out SecurityToken validatedToken);

        //            return principal;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        // Method to validate the token and return ClaimsPrincipal
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
