using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities;
using PARTS.DAL.Entities.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Data.Configurations
{
    class OrdersPartsConfiguration : IEntityTypeConfiguration<OrderPart>
    {
        public void Configure(EntityTypeBuilder<OrderPart> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.PartId });

            builder
              .HasOne(op => op.Order)
              .WithMany(o => o.OrderParts)
              .HasForeignKey(op => op.OrderId)
              .OnDelete(DeleteBehavior.Restrict);

            // Зв'язок Part ↔ OrderPart (один до багатьох)
            builder
                .HasOne(op => op.Part)
                .WithMany(p => p.OrderParts)
                .HasForeignKey(op => op.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
