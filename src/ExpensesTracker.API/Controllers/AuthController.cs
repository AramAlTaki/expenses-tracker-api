using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository repository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) 
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,               
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult.Succeeded)
            {
                 return Ok(new RegisterResponse
                 {
                     IsSuccess = true,
                     Message = "Successfully Registerd, Login Please."
                 });
            }

            return BadRequest(new RegisterResponse
            {
                IsSuccess = false,
                Message = "Something Went Wrong, Try Again."
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, request.Password);

                if(checkPasswordResult)
                {
                    var jwtToken = repository.CreateJWTToken(user);

                    return Ok(new LoginResponse
                    {
                        IsSuccess = true,
                        Message = "Successfully Logged in!",
                        Token = jwtToken
                    });
                }
            }

            return BadRequest(new LoginResponse
            {
                IsSuccess = false,
                Message = "Email or Password incorrect!"
            });
        }
    }
}
