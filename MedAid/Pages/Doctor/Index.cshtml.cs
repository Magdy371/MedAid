using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedAid.Pages.Doctor
{
    [Authorize(Roles = "Doctor")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
