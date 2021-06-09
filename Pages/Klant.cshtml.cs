using System.Threading.Tasks;
using Dog_school.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dog_school.Pages
{
    public class Klant : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            // Redirect to login page if userId is invalid or user is an admin
            var id = HttpContext.Session.GetInt32("UserID");
            var user = await UserRepository.GetUser(id);
            if (user is not {Admin_permission: false}) return RedirectToPage("Index");

            // Store user data in ViewData
            ViewData["name"] = user.Name;
            ViewData["address"] = user.Address;
            ViewData["zip code"] = user.Zip_code;
            ViewData["phone number"] = user.Phone_number;
            ViewData["email"] = user.Email;
            return Page();
        }
    }
}