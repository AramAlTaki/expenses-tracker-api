using Microsoft.AspNetCore.Identity;

namespace ExpensesTracker.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user);
    }
}
