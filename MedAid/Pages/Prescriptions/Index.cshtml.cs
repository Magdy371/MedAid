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

        public IList<Prescription> Prescription { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Prescription = await _context.Prescriptions.ToListAsync();
        }
    }
}
