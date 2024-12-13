using MedAid.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MedAid.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed medications if not already present
            if (!context.Medications.Any())
            {
                context.Medications.AddRange(
                    new Medication { Name = "Ibuprofen", Description = "Pain reliever", Dosage = "200mg" },
                    new Medication { Name = "Ciprofloxacin", Description = "Antibiotic", Dosage = "500mg" },
                    new Medication { Name = "Metformin", Description = "Diabetes medication", Dosage = "850mg" },
                    new Medication { Name = "Simvastatin", Description = "Cholesterol-lowering", Dosage = "10mg" },
                    new Medication { Name = "Atorvastatin", Description = "Cholesterol-lowering", Dosage = "20mg" },
                    new Medication { Name = "Losartan", Description = "Blood pressure control", Dosage = "50mg" },
                    new Medication { Name = "Lisinopril", Description = "Blood pressure control", Dosage = "10mg" },
                    new Medication { Name = "Omeprazole", Description = "Acid reflux treatment", Dosage = "20mg" },
                    new Medication { Name = "Esomeprazole", Description = "Acid reflux treatment", Dosage = "40mg" },
                    new Medication { Name = "Furosemide", Description = "Diuretic", Dosage = "40mg" },
                    new Medication { Name = "Hydrochlorothiazide", Description = "Diuretic", Dosage = "25mg" },
                    new Medication { Name = "Amlodipine", Description = "Blood pressure control", Dosage = "5mg" },
                    new Medication { Name = "Clopidogrel", Description = "Blood thinner", Dosage = "75mg" },
                    new Medication { Name = "Warfarin", Description = "Blood thinner", Dosage = "5mg" },
                    new Medication { Name = "Levothyroxine", Description = "Thyroid hormone replacement", Dosage = "50mcg" },
                    new Medication { Name = "Prednisone", Description = "Anti-inflammatory", Dosage = "10mg" },
                    new Medication { Name = "Alprazolam", Description = "Anxiety treatment", Dosage = "0.5mg" },
                    new Medication { Name = "Diazepam", Description = "Anxiety treatment", Dosage = "5mg" },
                    new Medication { Name = "Sertraline", Description = "Antidepressant", Dosage = "50mg" },
                    new Medication { Name = "Fluoxetine", Description = "Antidepressant", Dosage = "20mg" },
                    new Medication { Name = "Citalopram", Description = "Antidepressant", Dosage = "20mg" },
                    new Medication { Name = "Venlafaxine", Description = "Antidepressant", Dosage = "75mg" },
                    new Medication { Name = "Duloxetine", Description = "Antidepressant", Dosage = "60mg" },
                    new Medication { Name = "Tamsulosin", Description = "Prostate treatment", Dosage = "0.4mg" },
                    new Medication { Name = "Finasteride", Description = "Prostate treatment", Dosage = "5mg" },
                    new Medication { Name = "Allopurinol", Description = "Gout treatment", Dosage = "100mg" },
                    new Medication { Name = "Colchicine", Description = "Gout treatment", Dosage = "0.6mg" },
                    new Medication { Name = "Methotrexate", Description = "Autoimmune treatment", Dosage = "10mg" },
                    new Medication { Name = "Azithromycin", Description = "Antibiotic", Dosage = "500mg" },
                    new Medication { Name = "Doxycycline", Description = "Antibiotic", Dosage = "100mg" },
                    new Medication { Name = "Cetirizine", Description = "Allergy treatment", Dosage = "10mg" },
                    new Medication { Name = "Loratadine", Description = "Allergy treatment", Dosage = "10mg" },
                    new Medication { Name = "Montelukast", Description = "Asthma control", Dosage = "10mg" }
                );
                context.SaveChanges();
            }

            // Seed roles if they don't exist
            string[] roleNames = { "Admin", "Doctor", "Patient" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin user if not already created
            var adminEmail = "admin@medAid.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                // Hash the password before saving
                var passwordHasher = new PasswordHasher<IdentityUser>();
                var hashedPassword = passwordHasher.HashPassword(admin, "AdminPassword123#");

                admin.PasswordHash = hashedPassword;

                var result = await userManager.CreateAsync(admin);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Seed Doctor user if not already created
            var doctorEmail = "doctor@medAid.com";
            if (await userManager.FindByEmailAsync(doctorEmail) == null)
            {
                var doctor = new IdentityUser
                {
                    UserName = doctorEmail,
                    Email = doctorEmail
                };

                // Hash the password before saving
                var passwordHasher = new PasswordHasher<IdentityUser>();
                var hashedPassword = passwordHasher.HashPassword(doctor, "DoctorPassword123#");

                doctor.PasswordHash = hashedPassword;

                var result = await userManager.CreateAsync(doctor);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctor, "Doctor");
                }
            }

            // Seed Patient user if not already created
            var patientEmail = "patient@medAid.com";
            if (await userManager.FindByEmailAsync(patientEmail) == null)
            {
                var patient = new IdentityUser
                {
                    UserName = patientEmail,
                    Email = patientEmail
                };

                // Hash the password before saving
                var passwordHasher = new PasswordHasher<IdentityUser>();
                var hashedPassword = passwordHasher.HashPassword(patient, "PatientPassword123#");

                patient.PasswordHash = hashedPassword;

                var result = await userManager.CreateAsync(patient);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(patient, "Patient");
                }
            }
        }
    }
}
