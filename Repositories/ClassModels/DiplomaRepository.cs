using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Dog_school.Repositories
{
    public class DiplomaRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;Database=hondenschool;Uid=root;Pwd=Test@1234!");
        }
        public IEnumerable<Diploma> Get()
        {
            using var connect = Connect();
            var acc = connect.Query<Diploma>("SELECT * FROM diploma");
            return acc;
        }

        

        public void MakeCourse(Diploma Data)
        {
            using var connect = Connect();
            var acc = connect.Query<Diploma>("INSERT INTO diploma VALUES(@Diploma_ID, @Dog_ID, @Date_of_exam, @Note)"
                , new { Diploma_id = Data.Diploma_ID, Dog_ID = Data.Dog_ID, Date_of_exam = Data.Date_of_exam, Note = Data.Note});

        }

    }
}