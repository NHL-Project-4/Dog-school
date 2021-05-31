using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Dog_school.Database
{
    public static class Database
    {
        /// <summary>
        ///     String used for connecting to the database
        ///     TODO: Add config file for login credentials
        /// </summary>
        private const string ConnectString =
            "server=127.0.0.1;port=3306;database=hondenschool;uid=root;pwd=Test@1234!;";

        /// <summary>
        ///     Gets an open MySqlConnection connected to the database
        /// </summary>
        /// <exception cref="Exception">If an exception occurred when trying to open connection to the database</exception>
        /// <returns>A MySqlConnection</returns>
        public static async Task<MySqlConnection> GetConnection()
        {
            try
            {
                // Create mysql connection and attempt to connect to database
                await using var connection = new MySqlConnection(ConnectString);
                await connection.OpenAsync();
                return connection;
            }
            catch
            {
                throw new Exception("Failed to open connection to database");
            }
        }
    }
}