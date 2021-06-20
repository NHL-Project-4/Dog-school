using System.Linq;
using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Dog_school.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dog_school.Pages.Customer
{
    public class Create : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is a customer
            if (user?.Admin_permission != true) return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostCustomerCreate([FromForm] string username, [FromForm] string address,
            [FromForm] string postalCode, [FromForm] string phoneNumber, [FromForm] string email,
            [FromForm] string note)
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is a customer
            if (user?.Admin_permission != true) return RedirectToPage("/Index");

            // Redirect to customer create page if all input is empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
                return RedirectToPage("/Customer/Create");

            // Create account instance based on input
            var account = new User
            {
                Name = username, Address = address, Zip_code = postalCode, Phone_number = phoneNumber, Email = email,
                Note = note
            };

            // Set default password to 'password' and save newly created account in database
            // TODO: Ask customer for password, or send one time verify link
            account.SetPassword("password");
            await UserRepository.Save(account);

            // Get user id for redirecting
            var users = await UserRepository.GetUsers(username);
            var id = users.SingleOrDefault(temp =>
                temp.Email == email && temp.Address == address && temp.Zip_code == postalCode && temp.Note == note &&
                !temp.Admin_permission)?.User_ID;

            // Redirect to customer edit page of the newly created customer
            return RedirectToPage("/Customer/Edit", new {id});
        }
    }
}