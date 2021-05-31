using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Dog_school.Repositories
{
    public class UserRepository
    {

        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;Database=periode4;Uid=root;Pwd=sql53xrvtrw");
        }

        public User Login(string name, string password)
        {
            using IDbConnection _db = Connect();
            User user = null;
            
            user = _db.QuerySingle<User>
            (
            "SELECT * FROM user WHERE Name = @name",
            new
            {
                name = name,
                password = password
            }
            );
            if (password == user.Password)
            {
                return user;
            }
            return null;
            
        }





        public User getUser(int userID)
        {
            using IDbConnection _db = Connect();
            User returnUser = _db.QuerySingleOrDefault<User>
                            (
                            "SELECT * FROM user WHERE User_ID = @UserID",
                            new { UserID = userID }
                            );
            return returnUser;
        }
        public User getUser(string Name)
        {
            using IDbConnection _db = Connect();
            User returnUser = _db.QuerySingleOrDefault<User>
                            (
                            "SELECT * FROM user WHERE Name = @name",
                            new { name = Name }
                            );
            return returnUser;
        }
        public int DeleteUser(int userID)
        {
            try
            {
                using IDbConnection _db = Connect();
                _db.Execute
                    (
                    "DELETE FROM user WHERE UserID = @User_ID",
                    new { userID = userID }
                    );
                return 1;
            }
            catch 
            {
                return 0;
            }
        }
        public int DeleteUser(string Name)
        {
            try
            {
                using IDbConnection _db = Connect();
                _db.Execute
                    (
                    "DELETE FROM user WHERE Name = @name",
                    new { name = Name }
                    );
                return 1;
            }
            catch
            {
                return 0;
            }
        }

    }


}

