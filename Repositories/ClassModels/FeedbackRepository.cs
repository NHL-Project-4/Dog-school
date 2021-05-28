using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Dog_school.Repositories
{
    public class FeedbackRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;Database=hondenschool;Uid=root;Pwd=Test@1234!");
        }
        public IEnumerable<Feedback> Get()
        {
            using var connect = Connect();
            var acc = connect.Query<Feedback>("SELECT * FROM feedback");
            return acc;
        }

        

        public void MakeCourse(Feedback Data)
        {
            using var connect = Connect();
            var acc = connect.Query<Feedback>("INSERT INTO feedback VALUES(@Lesson_ID, @User_ID, @Note)"
                , new { Lesson_id = Data.Lesson_ID, User_ID = Data.User_ID, Note = Data.Note});

        }
    }
}