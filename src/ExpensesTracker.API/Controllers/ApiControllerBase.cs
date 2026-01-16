using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpensesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("User ID not found in claims.");

            if (!Guid.TryParse(userIdString, out var userId))
                throw new FormatException("User ID in claims is not a valid GUID.");

            return userId;
        }
    }
}
