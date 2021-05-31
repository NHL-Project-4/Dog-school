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
            var result = await connection.QueryAsync<User>("SELECT * FROM user WHERE Name = @Username",
                new {Username = username});
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
        ///     Deletes the specified user
        /// </summary>
        /// <param name="userId">The id of the user to delete</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> DeleteUser(int userId)
        {
            var connection = await GetConnection();
            return await connection.ExecuteAsync(
                "DELETE FROM user WHERE UserID = @User_ID"
                , new {User_ID = userId});
        }

        /// <summary>
        ///     Stores a user instance in the database
        /// </summary>
        /// <param name="user">The user to save</param>
        /// <returns>The amount of rows affected</returns>
        public static async Task<int> Save(User user)
        {
            // Hash users password before storing in the database
            var hashed = EnhancedHashPassword(user.Password);

            var connection = await GetConnection();
            return await connection.ExecuteAsync(
                "INSERT INTO user VALUES(@User_ID, @Email, @Password, @Name, @Address, @Zip_code, @Phone_number, @Admin_permission, @Note)"
                , new {user.User_ID, user.Email, Password = hashed, user.Name, user.Address, user.Zip_code, user.Phone_number, user.Admin_permission, user.Note});
        }
    }
}