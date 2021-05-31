using System.ComponentModel.DataAnnotations;

namespace Dog_school.Repositories
{
    public class User
    {
        public int User_ID { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip_code { get; set; }
        public string Phone_number { get; set; }
        public bool Admin_permission { get; set; }
        public string Note { get; set; }
    }
}