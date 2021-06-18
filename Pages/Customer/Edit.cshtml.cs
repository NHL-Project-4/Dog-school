using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Dog_school.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DogData =
    System.Collections.Generic.Dictionary<Dog_school.Database.Models.Dog, System.Collections.Generic.List<string>>;

namespace Dog_school.Pages.Customer
{
    public class Edit : PageModel
    {
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is a customer
            if (user?.Admin_permission != true) return RedirectToPage("/Index");
            var customer = await UserRepository.GetUser(id);

            // Set hasData and return if no customer was found
            ViewData["hasData"] = customer != null;
            if (customer == null) return Page();

            // Store user data in ViewData
            ViewData["id"] = user.User_ID;
            ViewData["name"] = user.Name;
            ViewData["address"] = user.Address;
            ViewData["zip code"] = user.Zip_code;
            ViewData["phone number"] = user.Phone_number;
            ViewData["email"] = user.Email;
            ViewData["note"] = user.Note;

            // Filter out courses that have finished
            var courses = await CourseRepository.GetCourses();
            ViewData["courses"] = courses.Where(course => course.Finish_date.CompareTo(DateTime.Today) >= 0);

            // Create dictionary for storing dogs and their courses
            var dogs = new DogData();

            foreach (var dog in await DogRepository.GetDogs(user.User_ID))
            {
                // Add courses to dog, or empty list if none were found
                var dogCourses = await CourseRepository.GetCourseNames(dog.Dog_ID);
                dogs[dog] = dogCourses?.ToList() ?? new List<string>();
            }

            // Store dogs and their courses in ViewData
            ViewData["dogs"] = dogs;
            return Page();
        }

        public async Task<IActionResult> OnPostCustomerEdit([FromForm] int? id, [FromForm] string username,
            [FromForm] string address, [FromForm] string postalCode, [FromForm] string phoneNumber,
            [FromForm] string email, [FromForm] string note)
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is a customer
            if (user?.Admin_permission != true) return RedirectToPage("/Index");

            // Check if customer to edit is valid
            var customer = await UserRepository.GetUser(id);
            if (customer == null) return RedirectToPage("/Index");

            // Update user data
            if (!string.IsNullOrWhiteSpace(username)) user.Name = username;
            if (!string.IsNullOrWhiteSpace(address)) user.Address = address;
            if (!string.IsNullOrWhiteSpace(postalCode)) user.Zip_code = postalCode;
            if (!string.IsNullOrWhiteSpace(phoneNumber)) user.Phone_number = phoneNumber;
            if (!string.IsNullOrWhiteSpace(email)) user.Email = email;
            if (!string.IsNullOrWhiteSpace(note)) user.Note = note;

            // Save updated account in database
            await UserRepository.Save(user);
            return await OnGetAsync(id);
        }

        public async Task<IActionResult> OnPostDogCreate([FromForm] int? id, [FromForm] string name,
            [FromForm] string breed, [FromForm] DateTime birthday, [FromForm] string gender, [FromForm] string note,
            [FromForm] int? course)
        {
            // Get user from session
            var user = await HttpContext.Session.GetUser();

            // Redirect to login page if user is invalid or user is a customer
            if (user?.Admin_permission != true || id == null) return RedirectToPage("/Index");

            // Check if customer to add dog to is valid
            var customer = await UserRepository.GetUser(id);
            if (customer == null) return RedirectToPage("/Index");

            // Parse input gender to enum, or return if it failed
            if (!Enum.TryParse<Gender>(gender, true, out var value)) return RedirectToPage("/Index");

            // Create dog instance based on input
            var dog = new Dog
            {
                User_ID = (int) id,
                Name = name,
                Breed = breed,
                Date_of_birth = birthday,
                Date_of_death = null,
                Gender = value,
                Note = note
            };

            // Save newly created dog in database and return if course wasn't specified
            await DogRepository.Save(dog);
            if (course == null) return await OnGetAsync(id);

            // Enroll the inserted dog into the requested course
            var dogId = await DogRepository.GetDogId(dog);
            await CourseRepository.Enroll(dogId, course);
            return await OnGetAsync(id);
        }
    }
}