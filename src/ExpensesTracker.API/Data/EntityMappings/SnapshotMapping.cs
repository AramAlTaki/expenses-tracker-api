using ExpensesTracker.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesTracker.API.Data.EntityMappings
{
    public class SnapshotMapping : IEntityTypeConfiguration<Snapshot>
    {
        public void Configure(EntityTypeBuilder<Snapshot> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Id)
                .IsRequired();

            builder
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(s => s.UserId);

            builder
                .Property(s => s.UserId)
                .IsRequired();

            builder
                .Property(s => s.Balance)
                .HasPrecision(18, 4)
                .IsRequired();

            builder
                .Property(s => s.Month)
                .IsRequired();

            builder
                .Property(s => s.Year)
                .IsRequired();

            builder
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();
        }
    }
}
