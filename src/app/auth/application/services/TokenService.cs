using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.src.app.auth.application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userId, string role)
        {
            // Leer configuración
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key no está configurado");

            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Jwt:Issuer no está configurado");

            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Jwt:Audience no está configurado");

            var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "60");

            // Convertir la key a bytes
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims (datos dentro del token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crear el descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            // Generar token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}