using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dog_school.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty] public User? LogUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogin()
        {
            // Redirect to page if no user was specified
            if (LogUser == null) return Page();

            // Redirect to page if credentials were invalid
            LogUser = await UserRepository.GetUser(LogUser.Name, LogUser.Password);
            if (LogUser?.User_ID == null) return Page();

            // Store user id and username in session
            HttpContext.Session.SetInt32("UserID", (int) LogUser.User_ID);
            HttpContext.Session.SetString("Username", LogUser.Name);
            return RedirectToPage(LogUser.Admin_permission ? "Admin" : "Klant");
        }
    }
}