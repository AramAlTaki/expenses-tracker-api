using ExpensesTracker.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ExpensesTracker.API.Data.EntityMappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(t => t.UserId).IsRequired();

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.Amount)
                .HasPrecision(18, 4).IsRequired();

            builder.Property(t => t.CurrencyCode)
                .HasMaxLength(3)
                .HasDefaultValue("USD");

            builder.Property(t => t.IsIncome)
                .IsRequired();

            builder.Property(t => t.IssueDate)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

        }
    }
}
