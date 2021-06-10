using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Dog_school.Utils;
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
            // Clear session when redirected to this page
            HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            // Redirect to page if no user was specified
            if (LogUser == null) return Page();

            // Redirect to page if credentials were invalid
            LogUser = await UserRepository.GetUser(LogUser.Name, LogUser.Password);
            if (LogUser?.User_ID == null) return Page();

            // Store user id in session
            HttpContext.Session.SetUser(LogUser);
            return RedirectToPage(LogUser.Admin_permission ? "Admin" : "Klant");
        }
    }
}