using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS.Api.Services.Auths
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(
            int userId,
            string role)
        {
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "JWT key is not configured.");

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expirationMinutes =
                _configuration.GetValue<int>(
                    "Jwt:ExpirationMinutes");

            var claims = new List<Claim>
            {
                // User ID
                new Claim(
                    ClaimTypes.NameIdentifier,
                    userId.ToString()),

                // JWT standard subject
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    userId.ToString()),

                // User role
                new Claim(
                    ClaimTypes.Role,
                    role)
            };

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key));

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    expirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}