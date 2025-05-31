using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities.Item;
namespace PARTS.DAL.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            // Налаштування властивостей
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            // Самореферентний зв'язок (Category -> ParentCategory)
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SupCategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict) // Заборона каскадного видалення
                .IsRequired(false);

            // Зв'язок з Parts
            builder.HasMany(c => c.Parts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // Зв'язок з CategoryImage (один до одного)
            builder.HasOne(c => c.CategoryImage)
                .WithOne(ci => ci.Category)
                .HasForeignKey<CategoryImage>(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            // Індекс для покращення продуктивності
            builder.HasIndex(c => c.ParentId).IsUnique(false);
            builder.HasIndex(c => c.Title).IsUnique(false);
            //CategorySeeder brandSeeder = new CategorySeeder();
            //brandSeeder.Seed(builder);

        }
    }
}
