using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dog_school.Database.Models;
using static Dog_school.Database.Database;
using static BCrypt.Net.BCrypt;

namespace Dog_school.Database.Repositories
{
    public static class UserRepository
    {
        /// <summary>
        ///     Gets all stored users
        /// </summary>
        /// <returns>The stored users</returns>
        public static async Task<IEnumerable<User>> GetUsers()
        {
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<User>("SELECT * FROM user");
            return result;
        }

        /// <summary>
        ///     Gets all stored users with the specified username
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <returns>The stored users with a matching username</returns>
        public static async Task<IEnumerable<User>> GetUsers(string username)
        {
            var connection = await GetConnection();
            var result = await connection.QueryAsync<User>("SELECT * FROM user WHERE Name = @username",
                new {username});
            return result;
        }

        /// <summary>
        ///     Attempts to find a user with the specified credentials
        /// </summary>
        /// <returns>The user with matching credentials, or null if not found</returns>
        public static async Task<User?> GetUser(string username, string password)
        {
            // Find all users with a matching name
            var users = await GetUsers(username);

            // Find user with a matching password hash, or return null if not found
            return users.FirstOrDefault(user => EnhancedVerify(password, user.Password));
        }

        /// <summary>
        ///     Attempts to find a user with the specified userId
        /// </summary>
        /// <param name="id">The user id to search for</param>
        /// <returns>The user with a matching userId, or null if not found</returns>
        public static async Task<User?> GetUser(int? id)
        {
            // Return null if the specified id is null
            if (id == null) return null;

            // Open connection and execute query
            var connection = await GetConnection();
            var result =
                await connection.QueryAsync<User>("SELECT * FROM user WHERE User_ID = @userId", new {userId = id});

            // Get the first user with a matching user id, or null if not found
            return result.FirstOrDefault();
        }

        /// <summary>
        ///     Removes the specified user
        /// </summary>
        /// <param name="userId">The id of the user to remove</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Remove(int userId)
        {
            var connection = await GetConnection();
            return await connection.ExecuteAsync("DELETE FROM user WHERE User_ID = @userId", new {userId});
        }

        /// <summary>
        ///     Saves a user instance in the database
        /// </summary>
        /// <param name="user">The user to save or update</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(User user)
        {
            var connection = await GetConnection();

            // Insert or update
            return await connection.ExecuteAsync(user.User_ID == null
                    ? "INSERT INTO user(Email, Password, Name, Address, Zip_code, Phone_number, Admin_permission, Note) VALUES(@Email, @Password, @Name, @Address, @ZipCode, @PhoneNumber, @AdminPermission, @Note)"
                    : "UPDATE user SET Email = @Email, Password = @Password, Name = @Name, Address = @Address, Zip_code = @ZipCode, Phone_number = @PhoneNumber, Admin_permission = @AdminPermission, Note = @Note WHERE User_ID = @UserId"
                , new {UserId = user.User_ID, user.Email, user.Password, user.Name, user.Address, ZipCode = user.Zip_code, PhoneNumber = user.Phone_number, AdminPermission = user.Admin_permission, user.Note});
        }


        public static async Task<IEnumerable<User>> GetUsersFromCourse(int courseID)
        {
            List<User> listResult = new List<User>();
            foreach (Dog dog in await DogRepository.GetDogsFromCourse(courseID))
            {

                listResult.Add(await GetUser(dog.User_ID));
            
            
            
            }
            return listResult;
        }

    }
}