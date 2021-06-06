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
        ///     Stores a feedback instance in the database
        /// </summary>
        /// <param name="feedback">The feedback to save</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Feedback feedback)
        {
            var connection = await GetConnection();
            return await connection.ExecuteAsync(
                "INSERT INTO feedback VALUES(@Lesson_ID, @User_ID, @Note)"
                , new {feedback.Lesson_ID, feedback.User_ID, feedback.Note});
        }
    }
}