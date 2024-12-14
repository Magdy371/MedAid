#region OldCreateScafolding
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using MedAid.Data;
//using MedAid.Models;
//using Microsoft.AspNetCore.Authorization;

//namespace MedAid.Pages_Prescriptions
//{
//    [Authorize(Roles = "Doctor")]
//    public class CreateModel : PageModel
//    {
//        private readonly MedAid.Data.ApplicationDbContext _context;

//        public CreateModel(MedAid.Data.ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult OnGet()
//        {
//            return Page();
//        }

//        [BindProperty]
//        public Prescription Prescription { get; set; } = default!;

//        // For more information, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            _context.Prescriptions.Add(Prescription);
//            await _context.SaveChangesAsync();

//            return RedirectToPage("./Index");
//        }
//    }
//}
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedAid.Data;
using MedAid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MedAid.Pages_Prescriptions
{
#nullable disable
    [Authorize(Roles = "Doctor")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Prescription Prescription { get; set; } = default!;

        public SelectList Patients { get; set; }
        public SelectList Medications { get; set; }

        public IActionResult OnGet()
        {
            // Populate dropdown lists
            Patients = new SelectList(_context.Patients, "Id", "FirstName");
            Medications = new SelectList(_context.Medications, "Name", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);  // You can log these in a file or use a debugger
                }
                // Re-populate dropdown lists if validation fails
                Patients = new SelectList(_context.Patients, "Id", "FirstName");
                Medications = new SelectList(_context.Medications, "Name", "Name");
                //return Page(); 
            }

            Console.WriteLine($"PatientId: {Prescription.PatientId}");
            Console.WriteLine($"MedicationName: {Prescription.MedicationName}");
            Console.WriteLine($"Dosage: {Prescription.Dosage}");
            Console.WriteLine($"Frequency: {Prescription.Frequency}");

            // Set the logged-in Doctor's ID
            var loggedInUser = await _userManager.GetUserAsync(User);
            Prescription.DoctorId = loggedInUser.Id;

            _context.Prescriptions.Add(Prescription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
