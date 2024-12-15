using MedAid.Models;
using Microsoft.AspNetCore.Identity;

namespace MedAid.Data
{
    public class PatientSeedData
    {
#nullable disable
        public static async Task Initialize(IServiceProvider serviceProvider, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed Roles
            string[] roleNames = { "Admin", "Doctor", "Patient" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Patients if they don't exist
            if (!context.Patients.Any())
            {
                // Create IdentityUsers for Patients
                var patientUsers = new List<IdentityUser>
        {
            new IdentityUser { UserName = "patient1@medaid.com", Email = "patient1@medaid.com" },
            new IdentityUser { UserName = "patient2@medaid.com", Email = "patient2@medaid.com" }
        };

                foreach (var user in patientUsers)
                {
                    if (await userManager.FindByEmailAsync(user.Email) == null)
                    {
                        var passwordHasher = new PasswordHasher<IdentityUser>();
                        user.PasswordHash = passwordHasher.HashPassword(user, "PatientPassword123#");
                        var result = await userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "Patient");
                        }
                    }
                }

                // Add Patients linked to IdentityUsers
                var existingUsers = context.Users.ToList();
                var patients = new List<Patient>
        {
            new Patient { UserId = existingUsers.FirstOrDefault(u => u.Email == "patient1@medaid.com")?.Id, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male" },
            new Patient { UserId = existingUsers.FirstOrDefault(u => u.Email == "patient2@medaid.com")?.Id, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1995, 5, 10), Gender = "Female" }
        };

                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }

    }
}
