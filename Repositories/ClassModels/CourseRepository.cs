using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Dog_school.Repositories
{
    public class CourseRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;Database=hondenschool;Uid=root;Pwd=Test@1234!");
        }
        public IEnumerable<Course> Get()
        {
            using var connect = Connect();
            var acc = connect.Query<Course>("SELECT * FROM course");
            return acc;
        }

        

        public void MakeCourse(Course Data)
        {
            using var connect = Connect();
            var acc = connect.Query<Course>("INSERT INTO course VALUES(@Course_ID, @Name, @Intake, @Start_date, @Finish_date, @Note)"
                , new { Course_id = Data.Course_ID, Name = Data.Name, Intake = Data.Intake, Start_date = Data.Start_date, Finish_date = Data.Finish_date, Note = Data.Note });

        }

    }
    }
