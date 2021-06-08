using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class FeedbackRepository
    {
        /// <summary>
        ///     Gets all stored feedback
        /// </summary>
        /// <returns>The stored feedback</returns>
        public static async Task<IEnumerable<Feedback>> GetFeedback()
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Feedback>("SELECT * FROM feedback");
            return result;
        }

        /// <summary>
        ///     Gets all stored feedback of a specific lesson
        /// </summary>
        /// <param name="lessonId">The lesson id to search for</param>
        /// <returns>The stored feedback of the specified lesson</returns>
        public static async Task<IEnumerable<Feedback>> GetFeedback(int lessonId)
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Feedback>("SELECT * FROM feedback WHERE Lesson_ID = @lessonId",
                    new {lessonId});
            return result;
        }

        /// <summary>
        ///     Saves a feedback instance in the database
        /// </summary>
        /// <param name="feedback">The feedback to save or update</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Feedback feedback)
        {
            var connection = await GetConnection();

            if (feedback.LessonId == null)
                // Dont pass the lesson id for auto increment
                return await connection.ExecuteAsync(
                    "INSERT INTO feedback(User_ID, Note) VALUES(@UserId, @Note)"
                    , new {feedback.UserId, feedback.Note});

            return await connection.ExecuteAsync(
                "INSERT INTO feedback VALUES(@LessonId, @UserId, @Note)"
                , new {feedback.LessonId, feedback.UserId, feedback.Note});
        }
    }
}