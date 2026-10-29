using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HealthCommBackend.Models;
using Hl7.Fhir.Model;

namespace HealthCommBackend.Data
{
    public class BackendDBContext : IdentityDbContext<ApplicationUser>
    {
        public BackendDBContext(DbContextOptions<BackendDBContext> options)
            : base(options)
        {
        }

        public DbSet<PractitionerProfile> Practitioners { get; set; }
        public DbSet<PatientProfile> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the one-to-one relationship between ApplicationUser and PatientProfile
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.PatientProfile)
                .WithOne(p => p.UnderlyingUserKey)
                .HasForeignKey<PatientProfile>(p => p.UserId);
            // Configure the one-to-one relationship between ApplicationUser and PractitionerProfile
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.PractitionerProfile)
                .WithOne(p => p.UnderlyingUserKey)
                .HasForeignKey<PractitionerProfile>(p => p.Id);

            //Configure the one-to-many relationship between PractitionerProfile and Prescription
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Prescriber);
  

            //Configure the one-to-many relationship between PatientProfile and Prescription
            //modelBuilder.Entity<Prescription>()
                //.HasOne(p => p.PatientProfile)
                //.WithMany(p => p.Prescriptions)
                //.HasForeignKey(p => p.PatientProfileId);

            //Enforce unique keys
            modelBuilder.Entity<PatientProfile>()
                .HasIndex(p => p.UserId)
                .IsUnique();
            modelBuilder.Entity<PractitionerProfile>()
                .HasIndex(p => p.UserId)
                .IsUnique();
        }



    }
}

