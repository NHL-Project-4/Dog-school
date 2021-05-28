using MySql.Data.MySqlClient;

namespace Dog_school.Repositories
{
    public class Database
    {
            protected string Table { get; set; }
            private MySqlConnection _connection;
            public string ConnectionString { get; set; }

            private MySqlConnection GetConnection()
            {
                this._connection = new MySqlConnection("server=127.0.0.1;port=3306;database=hondenschool;uid=root;pwd=Test@1234!;");
                this._connection.Open();
                return this._connection;
            }
        
    }
}