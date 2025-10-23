using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace befit.infrastructure.Authentication
{
    internal class JwtGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtGenerator(IConfigurationManager configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string userId, string email, string role)
        {
            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.Lifetime), signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
