using hospital_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace hospital_management_system.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
                : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visit { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // ziyaretler tablosu
            modelBuilder.Entity<Visit>()
                .HasKey(v => v.Id);


            //// ilişki tanımlaması
            //modelBuilder.Entity<Visit>()
            //    .HasOne(v => v.Patient)
            //    .WithMany(p => p.Visits)
            //    .HasForeignKey(v => v.PatientId);
        }
    }
}
