using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class CourseRepository
    {
        /// <summary>
        ///     Gets all stored courses
        /// </summary>
        /// <returns>The stored courses</returns>
        public static async Task<IEnumerable<Course>> GetCourses()
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Course>("SELECT * FROM course");
            return result;
        }

        /// <summary>
        ///     Saves a course instance in the database
        /// </summary>
        /// <param name="course">The course to save or update</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Course course)
        {
            var connection = await GetConnection();

            // Insert or update
            return await connection.ExecuteAsync(course.Course_ID == null
                    ? "INSERT INTO course(Name, Intake, Start_date, Finish_date, Note) VALUES(@Name, @Intake, @StartDate, @FinishDate, @Note)"
                    : "UPDATE course SET Name = @Name, Intake = @Intake, Start_date = @StartDate, Finish_date = @FinishDate, Note = @Note WHERE Course_ID = @CourseId"
                , new {CourseId = course.Course_ID, course.Name, course.Intake, StartDate = course.Start_date, FinishDate = course.Finish_date, course.Note});
        }
    }
}