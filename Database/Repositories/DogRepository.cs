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

            // Insert or update
            return await connection.ExecuteAsync(dog.Dog_ID == null
                    ? "INSERT INTO dog(User_ID, Name, Breed, Date_of_birth, Date_of_death, Gender, Photo, Note) VALUES(@UserId, @Name, @Breed, @DateOfBirth, @DateOfDeath, @Gender, @Photo, @Note)"
                    : "UPDATE dog SET User_ID = @UserId, Name = @Name, Breed = @Breed, Date_of_birth = @DateOfBirth, Date_of_death = @DateOfDeath, Gender = @Gender, Photo = @Photo, Note = @Note WHERE Dog_ID = @DogId"
                , new {DogId = dog.Dog_ID, UserId = dog.User_ID, dog.Name, dog.Breed, DateOfBirth = dog.Date_of_birth, DateOfDeath = dog.Date_of_death, dog.Gender, dog.Photo, dog.Note});
        }
    }
}