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
        public static async Task<Course> GetCourse(int id)
        {
            var connection = await GetConnection();
            var result =
                await connection.QuerySingleAsync<Course>("SELECT * FROM course WHERE Course_ID = @CourseID", new { CourseID = id })
                ;
            return result;
        }




        /// <summary>
        ///     Gets a list of course names for the specified dog
        /// </summary>
        /// <param name="dogId">The dog to gather course names for</param>
        /// <returns>A list of course names, or null if not found</returns>
        public static async Task<IEnumerable<string>?> GetCourseNames(int? dogId)
        {
            if (dogId == null) return null;

            var connection = await GetConnection();
            var result = await connection.QueryAsync<string>(
                "SELECT course.Name FROM dog_course INNER JOIN course WHERE Dog_ID = @DogId AND course.Course_ID = dog_course.Course_ID",
                new {DogId = dogId});
            return result;
        }

        /// <summary>
        ///     Enrolls a dog into the specified course id
        /// </summary>
        /// <param name="dogId">The dog to enroll</param>
        /// <param name="courseId">The course to enroll into</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Enroll(int? dogId, int? courseId)
        {
            if (dogId == null || courseId == null) return 0;

            var connection = await GetConnection();
            var result = await connection.ExecuteAsync(
                "INSERT INTO dog_course(Dog_ID, Course_ID) VALUES(@DogId, @CourseId)",
                new {DogId = dogId, CourseId = courseId});
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