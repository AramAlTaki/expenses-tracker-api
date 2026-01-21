using ExpensesTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ExpensesTracker.API.Data.EntityMappings
{
    public class ImageMapping : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasOne(i => i.Transaction)
               .WithOne(t => t.Receipt)
               .HasForeignKey<Image>(i => i.TransactionId);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                   .IsRequired();

            builder.Property(i => i.TransactionId)
                   .IsRequired();

            builder.Property(i => i.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(i => i.Extension)
                   .IsRequired();

            builder.Property(i => i.SizeInBytes)
                   .IsRequired();

            builder.Property(i => i.Path)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(i => i.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();

            builder.Ignore(i => i.File);

        }
    }
}
