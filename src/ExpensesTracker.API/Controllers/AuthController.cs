using ExpensesTracker.API.Contracts.Requests;
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

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) 
        {
            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email,               
            };

            var identityResult = await userManager.CreateAsync(identityUser,request.Password);

            if (identityResult.Succeeded)
            {
                 return Ok("User was registered please login");
            }

            return BadRequest("Something went wrong");
        }
    }
}
