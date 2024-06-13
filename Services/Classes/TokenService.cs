using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Models;
using Bookstore_Backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore_Backend.Services.Classes
{
    public class TokenService:ITokenService
    {

        readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public GenerateTokenResponse GenerateToken(GenerateTokenRequest request, User user)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]));

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: configuration["AppSettings:ValidIssuer"],
                    audience: configuration["AppSettings:ValidAudience"],
                    claims: new List<Claim>
                    {
                        new Claim("userName", request.Username),
                        new Claim("Role", user.Role)
                    },
                    notBefore: dateTimeNow,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(500)),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(500))
            };
        }
    }
}