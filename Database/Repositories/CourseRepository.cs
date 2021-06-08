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

            if (course.CourseId == null)
                // Dont pass the course id for auto increment
                return await connection.ExecuteAsync(
                    "INSERT INTO course(Name, Intake, Start_date, Finish_date, Note) VALUES(@Name, @Intake, @StartDate, @FinishDate, @Note)"
                    , new {course.Name, course.Intake, course.StartDate, course.FinishDate, course.Note});

            return await connection.ExecuteAsync(
                "INSERT INTO course VALUES(@CourseId, @Name, @Intake, @StartDate, @FinishDate, @Note)"
                , new {course.CourseId, course.Name, course.Intake, course.StartDate, course.FinishDate, course.Note});
        }
    }
}