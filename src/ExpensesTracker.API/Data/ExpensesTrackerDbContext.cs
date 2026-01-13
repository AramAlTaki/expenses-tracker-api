using ExpensesTracker.API.Data.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Receipt)
                .WithOne(i => i.Transaction)
                .HasForeignKey<Image>(i => i.TransactionId);

            modelBuilder.Entity<Category>()
                .HasOne(c=> c.Budget)
                .WithOne(b => b.Category)
                .HasForeignKey<Budget>(b => b.CategoryId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId);
        }

    }
}
