using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Data
{
    public class ExpensesTrackerAuthDbContext : IdentityDbContext
    {
        public ExpensesTrackerAuthDbContext(DbContextOptions<ExpensesTrackerAuthDbContext> options) : base(options) 
        {
        }
    }
}
