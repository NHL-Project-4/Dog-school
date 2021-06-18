using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Dog_school.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DogData =
    System.Collections.Generic.Dictionary<Dog_school.Database.Models.Dog,
        System.Collections.Generic.List<Dog_school.Database.Models.Lesson>>;

namespace Dog_school.Pages.Customer
{
    public class Index : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is an admin
            if (user?.Admin_permission != false || user.User_ID == null) return RedirectToPage("/Index");

            // Store user data in ViewData
            ViewData["name"] = user.Name;
            ViewData["address"] = user.Address;
            ViewData["zip code"] = user.Zip_code;
            ViewData["phone number"] = user.Phone_number;
            ViewData["email"] = user.Email;

            // Create dictionary for storing dogs and their lessons
            var dogs = new DogData();

            foreach (var dog in await DogRepository.GetDogs(user.User_ID))
            {
                // Add lessons to dog, or empty list if none were found
                var lessons = await LessonRepository.GetLessons(dog.Dog_ID);
                dogs[dog] = lessons?.ToList() ?? new List<Lesson>();
            }

            // Store dogs and their lessons in ViewData
            ViewData["dogs"] = dogs;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateCustomer([FromForm] string address, [FromForm] string postalCode,
            [FromForm] string phoneNumber, [FromForm] string email, [FromForm] string password)
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is an admin
            if (user?.Admin_permission != false) return RedirectToPage("/Index");

            // Update user data
            if (!string.IsNullOrWhiteSpace(address)) user.Address = address;
            if (!string.IsNullOrWhiteSpace(postalCode)) user.Zip_code = postalCode;
            if (!string.IsNullOrWhiteSpace(phoneNumber)) user.Phone_number = phoneNumber;
            if (!string.IsNullOrWhiteSpace(email)) user.Email = email;
            if (!string.IsNullOrWhiteSpace(password)) user.SetPassword(password);

            // Save updated account in database
            await UserRepository.Save(user);
            return RedirectToPage("/Customer/Index");
        }
    }
}