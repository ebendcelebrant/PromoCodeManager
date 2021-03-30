using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ALX_CodingAssignment.Token
{
    internal class TokenValidationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly RequestDelegate requestDelegate;
        public TokenValidationHandler(IHttpContextAccessor aHttpContextAccessor, IConfiguration aConfiguration,
            RequestDelegate aRequestDelegate)
        {
            configuration = aConfiguration;
            httpContextAccessor = aHttpContextAccessor;
            requestDelegate = aRequestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            HttpStatusCode statusCode;
            var token = context.Request.Headers["AccessToken"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                IdentityModelEventSource.ShowPII = true;
                try
                {
                    string sec = configuration["Token:JWT_SECURITY_KEY"];
                    var now = DateTime.UtcNow;
                    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));


                    SecurityToken securityToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = configuration["Token:JWT_AUDIENCE"],
                        ValidIssuer = configuration["Token:JWT_ISSUER"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    //extract and assign the user of the jwt
                    Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                    httpContextAccessor.HttpContext.User = handler.ValidateToken(token, validationParameters, out securityToken);

                }
                catch (SecurityTokenValidationException e)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                catch (Exception ex)
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }
            }

            await requestDelegate(context);
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }


    }
}