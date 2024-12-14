using MedAid.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedAid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient) // Link Prescription to Patient
                .WithMany()
                .HasForeignKey(p => p.PatientId) // Updated to match the int type
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading deletes
        }

    }
}
