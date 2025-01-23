using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using elsaeedTea.data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace elsaeedTea.service.Services.AuthenticationServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var expirationDate = DateTime.UtcNow.AddYears(10); // تأكد من استخدام UTC لضمان التوافق في الزمن عبر السيرفرات

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("FullName", user.FullName ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Iss, "YourIssuer"),  // إضافة الـ Issuer
        new Claim(JwtRegisteredClaimNames.Aud, "YourAudience")  // إضافة الـ Audience
    }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




    }
}
