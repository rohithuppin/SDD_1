using Microsoft.IdentityModel.Tokens;
using SDDUserApi.Data.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SDDUserApi.JWTHelper
{
    public class JwtHelper
    {

        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtHelper(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"]; // Read from appsettings.json
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }

        public bool IsTokenExpired(string token)
        {
            var result = token.Split('"');
            //token = result.Length > 1 ? result[1] : result[0];
            if (result.Length == 1)            
                token = token.Substring(("Bearer ").Length).Trim();
            else
                token = result[1];
            

            var handler = new JwtSecurityTokenHandler();

                // Validate and decode the token
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // If the token doesn't contain the 'exp' claim, it's not valid
            if (jsonToken == null || !jsonToken.Payload.ContainsKey("exp"))
            {
                throw new Exception("Token does not contain an expiration claim.");
            }

            // Extract the expiration time (exp claim) and compare with current time
            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(jsonToken.Payload["exp"].ToString())).DateTime;
            return expirationTime < DateTime.UtcNow;
            
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.EmailId),
                //new Claim(ClaimTypes.NameIdentifier, user.EmailId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
