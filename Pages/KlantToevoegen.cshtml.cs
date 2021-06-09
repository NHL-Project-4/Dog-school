using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dog_school.Pages
{
    public class KlantToevoegen : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            // Redirect to login page if userId is invalid or user isn't an admin
            var id = HttpContext.Session.GetInt32("UserID");
            var user = await UserRepository.GetUser(id);
            if (user is not {Admin_permission: true}) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostCustomerCreate([FromForm] string username, [FromForm] string address,
            [FromForm] string postalCode, [FromForm] string phoneNumber, [FromForm] string email,
            [FromForm] string note)
        {
            // Redirect to login page if the session is invalid
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(postalCode) || string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(note) ||
                HttpContext.Session.GetInt32("UserID") == null) return RedirectToPage("KlantToevoegen");

            // Create account instance based on input
            var account = new User
            {
                Name = username, Address = address, Zip_code = postalCode, Phone_number = phoneNumber, Email = email,
                Note = note
            };

            // Set default password to 'password'
            account.SetPassword("password");

            // Save newly created account in database
            await UserRepository.Save(account);
            return Page();
        }
    }
}