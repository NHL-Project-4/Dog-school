using System.Security.Claims;
using System.Security.Principal;
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
            //example
            HttpContext.Session.SetInt32("DataName", 1);
            ViewData["Name"] = HttpContext.Session.GetInt32("DataName");
            var ass = (int) ViewData["Name"];
        }

        public async Task<IActionResult> OnPostLogin()
        {
            LogUser = await UserRepository.GetUser(LogUser.Name, LogUser.Password);
            if (LogUser == null)
                // TODO: Impl
                return new PageResult();

            //if both are right
            string[] roles = {LogUser.AdminPermission.ToString(), LogUser.UserId.ToString()};
            GenericPrincipal user = new(new ClaimsIdentity(LogUser.Name), roles);
            HttpContext.User = user;
            return RedirectToPage(LogUser.AdminPermission ? "Admin" : "Klant");
        }
    }
}

//to get the data always use viewdata as this:
//    ViewData["Name"] = HttpContext.Session.GetInt32("DataName");

//you can use viewdata like this:
//    (int)ViewData["Name"]
//to get that data into an int. other data types can be used aswell.
//i tried just using int to get the sessiondata but it contains some other data aswel leading to errors so just use viewdata.

//to set the data in the session use:
//    HttpContext.Session.SetInt32("DataName", int);