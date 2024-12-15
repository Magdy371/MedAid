using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedAid.Models
{
#nullable disable
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign key to the Identity User table
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        // Add any other patient-specific fields you need

        public virtual IdentityUser User { get; set; }  // Navigation property to Identity User

    }
}
