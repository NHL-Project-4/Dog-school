using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;

namespace Dog_school.Database.Repositories
{
    public static class DogRepository
    {
        /// <summary>
        ///     Gets a dog from an existing dog instance
        ///     Used for getting the dogs id after creating a new dog
        /// </summary>
        /// <returns>The stored dog</returns>
        public static async Task<int?> GetDogId(Dog dog)
        {
            var connection = await GetConnection();
            var result = await connection.QueryAsync<int>(
                "SELECT Dog_ID FROM dog WHERE Name = @Name AND User_ID = @UserId AND Date_of_birth = @DateOfBirth",
                new {dog.Name, UserId = dog.User_ID, DateOfBirth = dog.Date_of_birth});
            return result.FirstOrDefault();
        }

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
        public static async Task<IEnumerable<Dog>> GetDogs(int? userId)
        {
            if (userId == null) return new List<Dog>();

            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Dog>("SELECT * FROM dog WHERE User_ID = @userId", new {userId});
            return result;
        }

        public static async Task<IEnumerable<Dog>> GetDogsFromCourse(int? courseId)
        {
            if (courseId == null) return new List<Dog>();

            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<Dog>(
                    "SELECT * FROM dog INNER JOIN dog_course ON dog.Dog_ID = dog_course.Dog_ID WHERE dog_course.Course_ID = @courseId",
                    new {courseId});
            return result;
        }


        /// <summary>
        ///     Removes the specified dog
        /// </summary>
        /// <param name="dogId">The id of the dog to remove</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Remove(int dogId)
        {
            var connection = await GetConnection();
            return await connection.ExecuteAsync("DELETE FROM dog WHERE Dog_ID = @dogId", new {dogId});
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