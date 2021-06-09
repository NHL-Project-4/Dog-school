using System.Threading.Tasks;
using Dog_school.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dog_school.Pages
{
    public class Admin : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            // Redirect to login page if userId is invalid or user isn't an admin
            var id = HttpContext.Session.GetInt32("UserID");
            var user = await UserRepository.GetUser(id);
            if (user is not {Admin_permission: true}) return RedirectToPage("Index");

            // Store username in ViewData
            ViewData["name"] = user.Name;
            return Page();
        }
    }
}