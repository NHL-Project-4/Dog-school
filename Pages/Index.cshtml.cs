using Microsoft.AspNetCore.Http;
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

        public void OnGet()
        {


            //example
            HttpContext.Session.SetInt32("DataName", 1);
            ViewData["Name"] = HttpContext.Session.GetInt32("DataName");
            int ass = (int)ViewData["Name"];
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