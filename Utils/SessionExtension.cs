using System.Threading.Tasks;
using Dog_school.Database.Models;
using Dog_school.Database.Repositories;
using Microsoft.AspNetCore.Http;

namespace Dog_school.Utils
{
    public static class SessionExtension
    {
        /// <summary>
        ///     Gets the logged in user from session data
        /// </summary>
        /// <param name="session">The session to search for the user id in</param>
        /// <returns>The logged in user, or null if not found</returns>
        public static async Task<User?> GetUser(this ISession session)
        {
            var id = session.GetInt32("UserID");
            return await UserRepository.GetUser(id);
        }

        /// <summary>
        ///     Sets the logged in user to the specified user
        /// </summary>
        /// <param name="session">The session to store the user id in</param>
        /// <param name="user">The user to log in</param>
        public static void SetUser(this ISession session, User user)
        {
            if (user.User_ID == null) return;
            session.SetInt32("UserID", (int) user.User_ID);
        }
    }
}