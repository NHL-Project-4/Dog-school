using System.ComponentModel.DataAnnotations;
using static BCrypt.Net.BCrypt;

namespace Dog_school.Database.Models
{
    public class User
    {
        public int? User_ID { get; set; }
        public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Name { get; set; }

        public string Address { get; set; }
        public string Zip_code { get; set; }
        public string Phone_number { get; set; }
        public bool Admin_permission { get; set; }
        public string Note { get; set; }

        /// <summary>
        ///     Hashes the specified password and assigns it to this user
        /// </summary>
        /// <param name="password">The password to set</param>
        public void SetPassword(string password)
        {
            Password = EnhancedHashPassword(password);
        }
    }
}