using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class LessonRepository
    {
        public static async Task<IEnumerable<Lesson>> GetLessons(int userId)
        {
            var connection = await GetConnection();

            // Get all lessons for this user
            // TODO: Shorten query and make it less garbage
            return await connection.QueryAsync<Lesson>(
                "SELECT lesson.lesson_id, lesson.course_id, lesson.lesson_name AS Name, lesson.start_date, lesson.`repeat` FROM lesson INNER JOIN course ON lesson.Course_ID = course.Course_ID INNER JOIN dog_course ON course.Course_ID = dog_course.Course_ID INNER JOIN dog ON dog.Dog_ID = dog_course.Dog_ID WHERE dog.User_ID = @UserId GROUP BY Lesson_ID",
                new {UserId = userId});
        }

        /// <summary>
        ///     Saves a lesson instance in the database
        /// </summary>
        /// <param name="lesson">The lesson to save or update</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Lesson lesson)
        {
            var connection = await GetConnection();

            // Insert or update
            return await connection.ExecuteAsync(lesson.Lesson_ID == null
                    ? "INSERT INTO lesson(Course_ID, Lesson_name, Start_date, `Repeat`) VALUES(@CourseId, @Name, @StartDate, @Repeat)"
                    : "UPDATE lesson SET Course_ID = @CourseId, Lesson_name = @Name, Start_date = @StartDate, `Repeat` = @Repeat WHERE Lesson_ID = @LessonId"
                , new {LessonId = lesson.Lesson_ID, CourseId = lesson.Course_ID, lesson.Name, StartDate = lesson.Start_date, lesson.Repeat});
        }
    }
}