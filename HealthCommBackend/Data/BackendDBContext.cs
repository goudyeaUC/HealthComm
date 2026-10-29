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
        public DbSet<PatientAppointment> PatientAppointment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=HealthComm.db");
        }

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
                .HasForeignKey<PractitionerProfile>(p => p.UserId);

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

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "patient-user-001",
                    UserName = "alex.johnson@example.com",
                    NormalizedUserName = "ALEX.JOHNSON@EXAMPLE.COM",
                    Email = "alex.johnson@example.com",
                    NormalizedEmail = "ALEX.JOHNSON@EXAMPLE.COM",
                    FirstName = "Alex",
                    LastName = "Johnson",
                    EmailConfirmed = true,
                    SecurityStamp = "static-security-patient-001",
                    ConcurrencyStamp = "static-concurrency-patient-001"
                },
                new ApplicationUser
                {
                    Id = "patient-user-002",
                    UserName = "emily.davis@example.com",
                    NormalizedUserName = "EMILY.DAVIS@EXAMPLE.COM",
                    Email = "emily.davis@example.com",
                    NormalizedEmail = "EMILY.DAVIS@EXAMPLE.COM",
                    FirstName = "Emily",
                    LastName = "Davis",
                    EmailConfirmed = true,
                    SecurityStamp = "static-security-patient-002",
                    ConcurrencyStamp = "static-concurrency-patient-002"
                },
                new ApplicationUser
                {
                    Id = "practitioner-user-001",
                    UserName = "jane.smith@example.com",
                    NormalizedUserName = "JANE.SMITH@EXAMPLE.COM",
                    Email = "jane.smith@example.com",
                    NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                    FirstName = "Jane",
                    LastName = "Smith",
                    EmailConfirmed = true,
                    SecurityStamp = "static-security-practitioner-001",
                    ConcurrencyStamp = "static-concurrency-practitioner-001"
                },
                new ApplicationUser
                {
                    Id = "practitioner-user-002",
                    UserName = "john.williams@example.com",
                    NormalizedUserName = "JOHN.WILLIAMS@EXAMPLE.COM",
                    Email = "john.williams@example.com",
                    NormalizedEmail = "JOHN.WILLIAMS@EXAMPLE.COM",
                    FirstName = "John",
                    LastName = "Williams",
                    EmailConfirmed = true,
                    SecurityStamp = "static-security-practitioner-002",
                    ConcurrencyStamp = "static-concurrency-practitioner-002"
                }
            );

            // Seed patients
            modelBuilder.Entity<PatientProfile>().HasData(
                new
                {
                    Id = 1,
                    UserId = "patient-user-001",
                    FirstName = "Alex",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    AdministrativeGender = "male",
                    Email = (string?)"alex.johnson@example.com",
                    PhoneNumber = (string?)"5551234567",
                    LastSyncedAt = (DateTime?)null,
                    FHIRIdentifier = (string?)"patient-1001"
                },
                new
                {
                    Id = 2,
                    UserId = "patient-user-002",
                    FirstName = "Emily",
                    LastName = "Davis",
                    DateOfBirth = new DateTime(1998, 8, 22),
                    AdministrativeGender = "female",
                    Email = "emily.davis@example.com",
                    PhoneNumber = "5559876543",
                    LastSyncedAt = (DateTime?)null,
                    FHIRIdentifier = (string?)"patient-1002"
                }

            );

            // Seed practitioners
            modelBuilder.Entity<PractitionerProfile>().HasData(
                new
                {
                    Id = 1,
                    UserId = "practitioner-user-001",
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1980, 4, 12),
                    Specialty = (string?)"Cardiology",
                    Email = (string?)"jane.smith@example.com",
                    PhoneNumber = (string?)"5551234567",
                    IsActive = true,
                    LastSyncedAt = (DateTime?)null
                },
                new
                {
                    Id = 2,
                    UserId = "practitioner-user-002",
                    FirstName = "John",
                    LastName = "Williams",
                    DateOfBirth = new DateTime(1975, 9, 25),
                    Specialty = (string?)"Family Medicine",
                    Email = (string?)"john.williams@example.com",
                    PhoneNumber = (string?)"5559876543",
                    IsActive = true,
                    LastSyncedAt = (DateTime?)null
                }

            );

            modelBuilder.Entity<PatientAppointment>().HasData(
            new
            {
                Id = 1,
                PatientProfileId = 1,
                PractitionerProfileId = 1,
                StartDateTime = new DateTime(2026, 11, 2, 10, 0, 0, DateTimeKind.Utc),
                VisitType = "Cardiology Consultation"

            }
            );


        }

    }




}

