using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Seeders;

namespace PARTS.DAL.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.СustomerId).IsRequired(false);
            builder.Property(p => p.IsPaid).HasDefaultValue(false);
            builder.Property(p => p.TransactionHash).HasMaxLength(256);
            builder.Property(p => p.Status)
                   .HasConversion<int>() // Збереження Enum як int у БД
                   .IsRequired();
            builder.Property(p=>p.OrderIndex).IsRequired(false);
            builder.Property(p=>p.WEIPrice).IsRequired(false);

            builder.HasMany(p => p.Parts)
                   .WithMany(p => p.Orders)
                   .UsingEntity(j => j.ToTable("OrderParts"));
        }
    }
}