using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class DogRepository
    {
        /// <summary>
        ///     Gets all stored dogs
        /// </summary>
        /// <returns>The stored dogs</returns>
        public static async Task<IEnumerable<Dog>> GetDogs()
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Dog>("SELECT * FROM dog");
            return result;
        }

        /// <summary>
        ///     Gets all stored dogs of a specific user
        /// </summary>
        /// <param name="userId">The user id to search for</param>
        /// <returns>The stored dogs of the specified user</returns>
        public static async Task<IEnumerable<Dog>> GetDogs(int userId)
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Dog>("SELECT * FROM dog WHERE User_ID = @userId", new {userId});
            return result;
        }

        /// <summary>
        ///     Saves a dog instance in the database
        /// </summary>
        /// <param name="dog">The dog to save or update</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(Dog dog)
        {
            var connection = await GetConnection();

            if (dog.DogId == null)
                // Dont pass the dog id for auto increment
                return await connection.ExecuteAsync(
                    "INSERT INTO dog(User_ID, Name, Breed, Date_of_birth, Date_of_death, Gender, Photo, Note) VALUES(@UserId, @Name, @Breed, @DateOfBirth, @DateOfDeath, @Gender, @Photo, @Note)"
                    , new {dog.UserId, dog.Name, dog.Breed, dog.DateOfBirth, dog.DateOfDeath, dog.Gender, dog.Photo, dog.Note});

            return await connection.ExecuteAsync(
                "INSERT INTO dog VALUES(@DogId, @UserId, @Name, @Breed, @DateOfBirth, @DateOfDeath, @Gender, @Photo, @Note)"
                , new {dog.DogId, dog.UserId, dog.Name, dog.Breed, dog.DateOfBirth, dog.DateOfDeath, dog.Gender, dog.Photo, dog.Note});
        }
    }
}