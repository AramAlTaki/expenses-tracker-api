using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Data
{
    public class ExpensesTrackerDbContext : DbContext
    {
        public ExpensesTrackerDbContext(DbContextOptions<ExpensesTrackerDbContext> dbContextOptions ) : base(dbContextOptions) 
        {
            
        }
    }
}
