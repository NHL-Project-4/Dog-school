using Dog_school.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Principal;

namespace Dog_school.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public User LogUser { get; set; }


        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {


            //example
            HttpContext.Session.SetInt32("DataName", 1);
            ViewData["Name"] = HttpContext.Session.GetInt32("DataName");
            int ass = (int)ViewData["Name"];
        }

        public IActionResult OnPostLogin()
        {
            try
            {
                LogUser = new UserRepository().Login(LogUser.Name, LogUser.Password);
            }
            catch //if the user doesnt exist
            {

            }

            if (LogUser == null) return new PageResult(); //if the password isnt right

            //if both are right
            string[] roles = {LogUser.Admin_permission.ToString(),LogUser.User_ID.ToString()};
            GenericPrincipal user = new GenericPrincipal(new ClaimsIdentity(LogUser.Name), roles );
            HttpContext.User = user;
            if (LogUser.Admin_permission)
            {
                return RedirectToPage("Admin");
            }
            else
            {
                return RedirectToPage("Klant");
            }
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