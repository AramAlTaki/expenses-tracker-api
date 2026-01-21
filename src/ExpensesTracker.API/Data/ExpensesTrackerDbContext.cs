using ExpensesTracker.API.Data.EntityMappings;
using ExpensesTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.API.Data
{
    public class ExpensesTrackerDbContext : DbContext
    {
        public ExpensesTrackerDbContext(DbContextOptions<ExpensesTrackerDbContext> dbContextOptions ) : base(dbContextOptions) 
        {
            
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Snapshot> Snapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransactionMapping());
            modelBuilder.ApplyConfiguration(new ImageMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new BudgetMapping());
            modelBuilder.ApplyConfiguration(new SnapshotMapping());
        }
    }
}
