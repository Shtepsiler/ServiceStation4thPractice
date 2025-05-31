using JOBS.DAL.Data.Configurations;
using JOBS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JOBS.DAL.Data
{
    public class ServiceStationDBContext : DbContext
    {
        public ServiceStationDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

            //  Database.EnsureCreated();
        }

        public DbSet<Specialisation> Specialisations { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<MechanicsTasks> MechanicsTasks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SpecialisationConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicsTasksConfiguration());


        }


    }
}
