using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JOBS.DAL.Entities;

namespace JOBS.DAL.Data.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ManagerId).IsRequired(false);
            builder.Property(p => p.VehicleId).IsRequired(false);
            builder.Property(p => p.ClientId).IsRequired(false);
            builder.Property(p => p.MechanicId).IsRequired(false);
            builder.Property(p => p.OrderId).IsRequired(false);
            builder.Property(p => p.ModelName).HasMaxLength(255);
            builder.Property(p => p.Status)
                   .HasConversion<int>() 
                   .IsRequired();
            //builder.Property(p => p.Status)
            //       .HasConversion<string>()
            //       .HasDefaultValue(Status.New);
            builder.Property(p => p.IssueDate).IsRequired();
            builder.Property(p => p.FinishDate).IsRequired(false);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.Price)
                   .HasPrecision(18, 2) // Вказуємо точність для decimal
                   .IsRequired(false);
            builder.Property(p => p.IsPaid).HasDefaultValue(false);
            builder.Property(p => p.TransactionHash).HasMaxLength(256);
            builder.Property(p => p.ModelConfidence).IsRequired(false);
            builder.Property(p => p.ModelAproved).HasDefaultValue(false);

            builder.HasOne(p => p.Mechanic)
                   .WithMany(m => m.Jobs)
                   .HasForeignKey(p => p.MechanicId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Tasks)
                   .WithOne(t => t.Job)
                   .HasForeignKey(t => t.JobId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
