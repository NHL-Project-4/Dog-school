using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Web.WebPages;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dog_school.Pages.Course
{
    public class Index : PageModel
    {
        public async Task<IActionResult> OnPostSetValues()
        {
            // Read input from request body into memory stream
            var stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Position = 0;

            // Read contents from memory stream
            using var reader = new StreamReader(stream);
            var requestBody = await reader.ReadToEndAsync();

            // Return if request is empty
            if (requestBody.IsEmpty()) return new EmptyResult();
            var jsonObject = JsonConvert.DeserializeObject<Objects>(requestBody);

            // Parse course, and return empty result if course id is invalid
            if (!int.TryParse(jsonObject.courseID, out var courseId)) return new EmptyResult();
            var courseData = await CourseRepository.GetCourse(courseId);
            if (courseData == null) return new EmptyResult();

            // Return course data as json result
            return new JsonResult(new List<string>
            {
                courseData.Name,
                courseData.Finish_date.ToString(CultureInfo.CurrentCulture),
                courseData.Start_date.ToString(CultureInfo.CurrentCulture),
                courseData.Intake,
                courseId.ToString()
            });
        }

        public async Task<ActionResult> OnPostSetClientValues()
        {
            var stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Position = 0;

            using var reader = new StreamReader(stream);
            var requestBody = await reader.ReadToEndAsync();
            var returnObjects = new List<User>();

            if (requestBody.Length <= 0) return new JsonResult(returnObjects);
            var obj = JsonConvert.DeserializeObject<Objects>(requestBody);
            if (obj != null)
                returnObjects =
                    (List<User>) await UserRepository.GetUsersFromCourse(Convert.ToInt32(obj.courseID));

            return new JsonResult(returnObjects);
        }


        public async Task<IActionResult> OnPostEditCourse([FromForm] string name, [FromForm] DateTime date,
            [FromForm] int? id)
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

        public async Task<IActionResult> OnPostSaveCourse([FromForm] string name, [FromForm] DateTime date,
            [FromForm] DateTime end)
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
        public string? courseID { get; set; }
    }
}