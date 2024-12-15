using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedAid.Data;
using MedAid.Models;
using Microsoft.AspNetCore.Authorization;

namespace MedAid.Pages_Prescriptions
{
#nullable disable
    [Authorize(Roles = "Doctor")]
    public class DetailsModel : PageModel
    {
        private readonly MedAid.Data.ApplicationDbContext _context;

        public DetailsModel(MedAid.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Prescription Prescription { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var prescription = await _context.Prescriptions.FirstOrDefaultAsync(m => m.Id == id);
        //    if (prescription == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        Prescription = prescription;
        //    }
        //    return Page();
        //}
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the prescription with related patient and doctor details
            Prescription = await _context.Prescriptions
                .Include(p => p.Patient) // Load patient details
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Prescription == null)
            {
                return NotFound();
            }

            // Fetch the doctor's username from the Identity table
            var doctor = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == Prescription.DoctorId);

            if (doctor != null)
            {
                Prescription.DoctorName = doctor.UserName; // Use DoctorName property
            }

            return Page();
        }

    }
}
