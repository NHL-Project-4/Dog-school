using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class DiplomaRepository
    {
        /// <summary>
        ///     Gets all stored diplomas
        /// </summary>
        /// <returns>The stored diplomas</returns>
        public static async Task<IEnumerable<Diploma>> GetDiplomas()
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Diploma>("SELECT * FROM diploma");
            return result;
        }

        /// <summary>
        ///     Gets all stored diplomas of a specific dog
        /// </summary>
        /// <param name="dogId">The dog id to search for</param>
        /// <returns>The stored diplomas of the specified dog</returns>
        public static async Task<IEnumerable<Diploma>> GetDiplomas(int dogId)
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Diploma>("SELECT * FROM diploma WHERE Dog_ID = @dogId",
                    new {dogId});
            return result;
        }

        /// <summary>
        ///     Saves a diploma instance in the database
        /// </summary>
        /// <param name="diploma">The diploma to save or update</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Diploma diploma)
        {
            var connection = await GetConnection();

            // Insert or update
            return await connection.ExecuteAsync(diploma.Diploma_ID == null
                    ? "INSERT INTO diploma(Dog_ID, Date_of_exam, Note) VALUES(@DogId, @DateOfExam, @Note)"
                    : "UPDATE diploma SET Dog_ID = @DogId, Date_of_exam = @DateOfExam, Note = @Note WHERE Diploma_ID = @DiplomaId"
                , new {DiplomaId = diploma.Diploma_ID, DogId = diploma.Dog_ID, DateOfExam = diploma.Date_of_exam, diploma.Note});
        }
    }
}