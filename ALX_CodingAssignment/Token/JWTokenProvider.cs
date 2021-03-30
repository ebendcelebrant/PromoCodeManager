using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ALX_CodingAssignment.Token
{
    public static class JWTokenProvider
    {
        public static string CreateToken(UserIdentityData userIdentity, IConfiguration configuration)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(1);
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("Id", userIdentity.Id.ToString()),
                new Claim("UserName", userIdentity.UserName),
                new Claim("Email", userIdentity.Email),
            });

            string sec = configuration["Token:JWT_SECURITY_KEY"];
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: configuration["Token:JWT_ISSUER"], audience: configuration["Token:JWT_AUDIENCE"],
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

    public class UserIdentityData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}