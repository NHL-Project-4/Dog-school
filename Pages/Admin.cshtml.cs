using System.Threading.Tasks;
using Dog_school.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dog_school.Pages
{
    public class Admin : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is a customer
            if (user?.Admin_permission != true) return RedirectToPage("Index");

            // Store username in ViewData
            ViewData["name"] = user.Name;
            return Page();
        }
    }
}