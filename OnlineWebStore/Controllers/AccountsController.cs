using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;
using OnlineWebStore.service;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace OnlineWebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IAccountService accountService;

        public AccountsController(IAccountService _accountService) {
        accountService = _accountService;
        }

        [HttpPost("users/signup")]
        public async Task<IActionResult> createAccount(SignupDto signupDto)
        {
            try
            {
                var result = await accountService.signUp(signupDto);
                if (result.Succeeded)
                {
                    return StatusCode((int)HttpStatusCode.OK, new { message = "User Added Successfully", status = "success" });
                }
                return StatusCode((int)HttpStatusCode.BadRequest, new { message = "Failed to add user", status = "error" });
            } catch (Exception ex) {
                return StatusCode((int)HttpStatusCode.BadRequest, new { message = "Failed to add user", status = "error" });

            }

        }

        [HttpPost("users/login")]
        public async Task<IActionResult> login(SigninDto signinDto)
        {
            var result = await accountService.login(signinDto);
            if (result.Succeeded)
            {
                List<Claim> authClaim = new List<Claim>()
                {
                    new Claim("username",signinDto.Username),
                    new Claim("unique value",Guid.NewGuid().ToString())


                };

                IList<string> userRoles = await accountService.getUserRoles(signinDto.Username);
                List<string> roles = new List<string>();
                foreach(var x in userRoles)
                {
                    roles.Add(x);
                }
                authClaim.Add(new Claim("roles", string.Join(",", userRoles)));
                var user = await accountService.getUser(signinDto.Username);
                if(user.Store!=null)
                authClaim.Add(new Claim("storeId",user.Store.Id.ToString()));
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("R4nd0mlyGeneratedKeyThatIs32Chars"));

                var tokenData = new JwtSecurityToken(
                            issuer: "http://localhost",
                            audience: "User",
                            expires: DateTime.Now.AddDays(15),
                            claims: authClaim,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(tokenData)
                });
            }
            else {
                return Unauthorized("Incorrect Uesrname|Password.");
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> getAllUsers()
        {
            return Ok(await accountService.getUsers());
        }

        [HttpGet("users/{username}")]
        public async Task<IActionResult> getUser(string username)
        {
            return Ok(await accountService.getUser(username));
        }

    }
}
