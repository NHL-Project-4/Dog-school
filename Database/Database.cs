using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Dog_school.Database
{
    public static class Database
    {
        /// <summary>
        ///     String used for connecting to the database
        /// </summary>
        public static readonly string? ConnectString;

        static Database()
        {
            // Deserialize config
            var config = Config.Deserialize("config.json");
            ConnectString = config?.GetConnectString();
            if (config != null) return;

            // Generate config file
            Config.Generate("config.json");
            Console.WriteLine("Generated config file!");

            // Shut down application
            Environment.Exit(0);
        }

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