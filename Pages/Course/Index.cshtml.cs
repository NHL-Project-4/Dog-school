using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Dog_school.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dog_school.Pages.Course
{
    public class Index : PageModel
    {
        public async Task<ActionResult> OnPostSetValuesAsync()
        {
            MemoryStream stream = new MemoryStream();

            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                List<string> returnObjects = new List<string>();


                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<Objects>(requestBody);
                    if (obj != null)
                    {
                        var courseData = await CourseRepository.GetCourse(Convert.ToInt32(obj.courseID));

                        returnObjects = new List<string>
                        {
                            courseData.Name,
                            courseData.Finish_date.ToString(),
                            courseData.Start_date.ToString(),
                            courseData.Intake,
                            courseData.Course_ID.ToString()
                            
                        };
                    }
                }
               
                return new JsonResult(returnObjects);
            }
        }

        public async Task<ActionResult> OnPostSetClientValuesAsync()
        {
            MemoryStream stream = new MemoryStream();

            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                List<User> returnObjects = new List<User>();


                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<Objects>(requestBody);
                    if (obj != null)
                    {
                        returnObjects = (List<User>)await UserRepository.GetUsersFromCourse(Convert.ToInt32(obj.courseID));
                    }
                }

                return new JsonResult(returnObjects);
            }
        }


        public async Task<IActionResult> OnPostEditCursus([FromForm] string name, [FromForm] DateTime date, [FromForm] int? id)
        {
            var course = new Database.Models.Course
            {
                Course_ID = id,
                Name = name,
                Start_date = date
            };
            await CourseRepository.Save(course);
            return Page();
        }

        public async Task<IActionResult> OnPostSaveCourse([FromForm] string name, [FromForm] DateTime date, [FromForm] DateTime end) 
        {
            var course = new Database.Models.Course
            {
                Name = name,
                Start_date = date,
                Finish_date = end
            };
            await CourseRepository.Save(course);

            return Page();
        }

    }

    public class Objects
    {
        public string ?courseID { get; set; }

    }

}