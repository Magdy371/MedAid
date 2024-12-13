#region OldCreateScafolding
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

namespace MedAid.Pages_Prescriptions
{
    [Authorize(Roles = "Doctor")]
    public class CreateModel : PageModel
    {
        private readonly MedAid.Data.ApplicationDbContext _context;

        public CreateModel(MedAid.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Prescription Prescription { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Prescriptions.Add(Prescription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
#endregion
