using System.ComponentModel.DataAnnotations;

namespace Dog_school.Database.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Name { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool AdminPermission { get; set; }
        public string Note { get; set; }
    }
}