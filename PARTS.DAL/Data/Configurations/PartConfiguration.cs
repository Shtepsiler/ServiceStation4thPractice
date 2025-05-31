using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities.Item;
namespace PARTS.DAL.Data.Configurations
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(pi => pi.PartImage).WithOne(p => p.Part).HasForeignKey<PartImage>(ci => ci.PartId);

            builder.HasOne(p => p.Category)
                              .WithMany(c => c.Parts);

            builder.HasMany(p => p.OrderParts)
                   .WithOne(p => p.Part);

        }
    }
}
