using ExpensesTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesTracker.API.Data.EntityMappings
{
    public class BudgetMapping : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .IsRequired();

            builder
                .Property(b => b.CategoryId)
                .IsRequired();

            builder
                .HasOne(b => b.Category)
                .WithOne(c => c.Budget)
                .HasForeignKey<Budget>(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(b => b.Amount)
                .HasPrecision(18, 4)
                .IsRequired();

            builder
                .Property(b => b.Month)
                .IsRequired();

            builder
                .Property(b => b.Year)
                .IsRequired();

            builder
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();
        }
    }
}
