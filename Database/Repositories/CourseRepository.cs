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
        ///     Stores a course instance in the database
        /// </summary>
        /// <param name="course">The course to save</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Course course)
        {
            var connection = await GetConnection();
            return await connection.ExecuteAsync(
                "INSERT INTO course VALUES(@Course_ID, @Name, @Intake, @Start_date, @Finish_date, @Note)"
                , new {course.Course_ID, course.Name, course.Intake, course.Start_date, course.Finish_date, course.Note});
        }
    }
}