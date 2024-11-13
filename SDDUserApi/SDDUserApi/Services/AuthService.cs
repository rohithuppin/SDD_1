using SDDUserApi.Data.Model;
using SDDUserApi.JWTHelper;

namespace SDDUserApi.Services
{
    public class AuthService
    {
        private readonly JwtHelper _jwtValidator;

        public AuthService(JwtHelper jwtValidator)
        {
            _jwtValidator = jwtValidator;
        }

        public bool ValidateUserToken(string token)
        {
            // Check if the token is expired
            return !_jwtValidator.IsTokenExpired(token);
        }

        public string GenerateJwtToken(User user)
        {
            return _jwtValidator.GenerateJwtToken(user);
        }
    }
}
