using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JOBS.DAL.Entities;

namespace JOBS.DAL.Data.Configurations
{
    public class MechanicsTasksConfiguration : IEntityTypeConfiguration<MechanicsTasks>
    {
        public void Configure(EntityTypeBuilder<MechanicsTasks> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.MechanicId).IsRequired(false);
            builder.Property(p => p.JobId).IsRequired(false);
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.Property(p => p.Task).HasMaxLength(200);

            builder.Property(p => p.Status)
                   .HasConversion<int>()
                   .IsRequired();           
            //builder.Property(p => p.Status)
            //       .HasConversion<string>()
            //       .HasDefaultValue(Status.New);

            // Налаштування зв’язків
            builder.HasOne(p => p.Job)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(p => p.JobId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Mechanic)
                   .WithMany(p=>p.MechanicsTasks)  
                   .HasForeignKey(p => p.MechanicId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
