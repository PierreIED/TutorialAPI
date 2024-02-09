using Tutorials.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tutorials.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Properties

        private IConfiguration Configuration
        {
            get;
        }

        #endregion

        #region Construction

        public AuthenticationService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region IAuthenticationService implementation

        public string GenerateToken(Member user)
        {
            var numericDate = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, numericDate, ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Iat, numericDate, ClaimValueTypes.Integer64)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])), 
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Configuration["Tokens:Issuer"],
                Configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddDays(Int32.Parse(Configuration["Tokens:Expires"] ?? "1")),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
