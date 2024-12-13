using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedAid.Pages.Patient
{
    [Authorize(Roles = "Patient")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
