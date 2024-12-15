#region OldScafoldingCode
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
    [Authorize(Roles = "Doctor")]
    public class IndexModel : PageModel
    {
        private readonly MedAid.Data.ApplicationDbContext _context;

        public IndexModel(MedAid.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Prescription> Prescription { get; set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    //Prescription = await _context.Prescriptions.ToListAsync();
        //    Prescription = await _context.Prescriptions
        //    .Include(p => p.Patient) // Load Patient navigation property
        //    .ToListAsync();
        //}

        public async Task OnGetAsync()
        {
            // Load prescriptions with related patients
            Prescription = await _context.Prescriptions
                .Include(p => p.Patient) // Load patient details
                .ToListAsync();

            // Fetch all unique DoctorIds from the prescriptions
            var doctorIds = Prescription.Select(p => p.DoctorId).Distinct();

            // Load doctor usernames
            var doctorNames = await _context.Users
                .Where(user => doctorIds.Contains(user.Id))
                .ToDictionaryAsync(user => user.Id, user => user.UserName); // Adjust if you have FullName

            // Add DoctorName to each prescription
            foreach (var prescription in Prescription)
            {
                prescription.DoctorName = doctorNames[prescription.DoctorId]; // Temporary property
            }
        }

    }
}
#endregion

