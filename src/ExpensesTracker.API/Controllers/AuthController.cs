using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository repository;
        private readonly IValidator<LoginRequest> loginValidator;
        private readonly IValidator<RegisterRequest> registerValidator;

        public AuthController(
            UserManager<IdentityUser> userManager, 
            ITokenRepository repository, 
            IValidator<LoginRequest> loginValidator, 
            IValidator<RegisterRequest> registerValidator)
        {
            this.userManager = userManager;
            this.repository = repository;
            this.loginValidator = loginValidator;
            this.registerValidator = registerValidator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) 
        {
            var validationResult = await registerValidator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

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
            var validationResult = await loginValidator.ValidateAsync(request);
            var errorResponse = ValidationFailedResponse(validationResult);

            if (errorResponse != null)
                return errorResponse;

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
