using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class LessonRepository
    {
        public static async Task<IEnumerable<Lesson>?> GetLessons(int? dogId)
        {
            if (dogId == null) return null;
            var connection = await GetConnection();

            // Get all lessons for this user
            // TODO: Shorten query and make it less garbage
            return await connection.QueryAsync<Lesson>(
                "SELECT lesson.lesson_id, lesson.course_id, lesson.lesson_name AS Name, lesson.start_date FROM lesson INNER JOIN course ON lesson.Course_ID = course.Course_ID INNER JOIN dog_course ON course.Course_ID = dog_course.Course_ID WHERE dog_course.Dog_ID = @DogId GROUP BY Lesson_ID",
                new {DogId = dogId});
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
                    ? "INSERT INTO lesson(Course_ID, Lesson_name, Start_date) VALUES(@CourseId, @Name, @StartDate)"
                    : "UPDATE lesson SET Course_ID = @CourseId, Lesson_name = @Name, Start_date = @StartDate WHERE Lesson_ID = @LessonId"
                , new {LessonId = lesson.Lesson_ID, CourseId = lesson.Course_ID, lesson.Name, StartDate = lesson.Start_date});
        }
    }
}