using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Dog_school.Repositories
{
    public class DogRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;Database=hondenschool;Uid=root;Pwd=Test@1234!");
        }
        public IEnumerable<Dog> Get()
        {
            using var connect = Connect();
            var acc = connect.Query<Dog>("SELECT * FROM dog");
            return acc;
        }

        public void MakeCourse(Dog Data)
        {
            using var connect = Connect();
            var acc = connect.Query<Dog>("INSERT INTO dog VALUES(@Dog_ID, @User_ID, @Name, @Breed, @Date_of_birth, @Date_of_death. @Gender, @Photo, @Note)"
                , new { Dog_id = Data.Dog_ID, User_ID = Data.User_ID, Name = Data.Name, Breed = Data.Breed, Date_of_birth = Data.Date_of_birth, Date_of_death = Data.Date_of_death ,Gender = Data.Gender, Photo = Data.Photo, Note = Data.Note});

        }
    }
}