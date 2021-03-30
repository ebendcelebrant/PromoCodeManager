using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ALX_CodingAssignment.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using ALX_CodingAssignment.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ALX_CodingAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<User> aUserManager, 
            SignInManager<User> aSignInManager,
            ILogger<AccountController> aLogger, IConfiguration aConfiguration)
        {
            userManager = aUserManager;
            signInManager = aSignInManager;
            logger = aLogger;
            configuration = aConfiguration;
        }

        [HttpGet]
        [Route("LoadAccounts")]
        public IActionResult LoadAccounts()
        {
            var users = userManager.Users.ToList();
            return Ok(users);
        }


        [HttpGet]
        [Authorize]
        [Route("AuthLoadAccounts")]
        public IActionResult AuthLoadAccounts()
        {
            var users = userManager.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        [Route("Login")]
        //public async Task<IActionResult> Login([FromBody] UserDto userDto)
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var messageResponse = "";
            logger.LogInformation($"API Request. Params: UserName: {userDto.UserName}, Email: {userDto.Email}");
            var user = await userManager.FindByEmailAsync(userDto.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, userDto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:JWT_SECURITY_KEY"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["Token:JWT_ISSUER"],
                    audience: configuration["Token:JWT_AUDIENCE"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                messageResponse = $"User {userDto.UserName} sign in successful!";

                logger.LogInformation(messageResponse);
                return Ok(new
                {
                    UserId = user.Id,
                    Response = messageResponse,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
            {
                var message = $"Login was not successful for user {userDto.UserName}. Kindly check your username/password and try again later";
                logger.LogInformation($"API response -> {message}");
                return new ObjectResult(this.StatusCode(404, message));
            }

        }

        [HttpPost]
        [Route("Signup")]
        // public async Task<IActionResult> Register([FromBody] UserDto userDto)
        public async Task<IActionResult> Register(UserDto userDto)
        {
            StringBuilder messageBuilder = new StringBuilder();
            logger.LogInformation($"API Request. Params: Username: {userDto.UserName} email: {userDto.Email} password: hidden");
            var result = await userManager.CreateAsync(new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            }, userDto.Password);

            if (result.Succeeded)
            {
                return Ok($"User {userDto.UserName} created successfully!");
            }
            else
            {
                messageBuilder.Append($"User could not be created! {Environment.NewLine}");
                foreach (IdentityError idError in result.Errors)
                {
                    messageBuilder.Append($"Code: {idError.Code} Description: {idError.Description} {Environment.NewLine}");
                }
                logger.LogInformation(messageBuilder.ToString());
                return new StatusCodeResult((int)HttpStatusCode.ExpectationFailed);
            }
        }
    }
}
