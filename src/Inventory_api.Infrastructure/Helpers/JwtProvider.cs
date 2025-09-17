using Microsoft.Extensions.Configuration;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventory_api.Infrastructure.Helpers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName.ToLower()), // Convert to lowercase for consistent role comparison
                    new Claim("role", user.Role.RoleName.ToLower()), // Add a custom role claim for easier access
                    new Claim(ClaimTypes.Name, user.Name)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration!["Jwt:AccessSecret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration?["Jwt:Issuer"],
                audience: _configuration!["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Extend to 1 hour for better usability
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration!["Jwt:RefreshSecret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration?["Jwt:Issuer"],
                audience: _configuration!["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), // Extend to 7 days
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
