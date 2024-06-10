﻿using Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Service.Results.Authenticate;

namespace Company_Consultation.WebAPI.Extensions
{
    public static class JwtExtension
    {
        public static string Generate(ResponseData data)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = GenerateClaims(data),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = credentials,
                };
                var token = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static ClaimsIdentity GenerateClaims(ResponseData user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("Id", user.Id));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            // Remover a adição de roles
            // foreach (var role in user.Roles)
            //     ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}
