// BLL/Services/TokenService.cs
using eCampus_Prototype.BLL.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using QuizForAndroid.DAL.Entities;
using Microsoft.VisualBasic;

namespace eCampus_Prototype.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> GenerateJwtTokenAsync(User user, IEnumerable<string> roles)
        {
            // Retrieve JWT settings from configuration
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = double.Parse(jwtSettings["ExpirationMinutes"]!);

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException("JWT Secret Key is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Define token claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd")),
                //new Claim("Gender", (user.Gender ? "Male" : "Female")),
                new Claim("TokenId", Guid.NewGuid().ToString())  // Add GUID as a custom claim
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
