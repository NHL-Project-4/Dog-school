using System.IO;
using System.Text;
using System.Text.Json;

namespace Dog_school.Database
{
    internal class Config
    {
        public string? Server { get; set; }
        public int Port { get; set; }
        public string? Database { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public string GetConnectString()
        {
            // Create string builder that will be used for building the connect string
            var builder = new StringBuilder();
            builder.Append($"port={Port};");

            // Add config elements if not null
            if (Server != null) builder.Append($"server={Server};");
            if (Database != null) builder.Append($"database={Database};");
            if (Username != null) builder.Append($"uid={Username};");
            if (Password != null) builder.Append($"pwd={Password};");
            return builder.ToString();
        }

        public static void Generate(string path)
        {
            // Create default config to write to the configuration file
            var config = new Config
            {
                Server = "127.0.0.1", Port = 3306, Database = "name", Username = "username", Password = "password"
            };

            // Serialize default config and write to configuration file
            var serialized = JsonSerializer.Serialize(config, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(path, serialized);
        }

        public static Config? Deserialize(string path)
        {
            if (!File.Exists(path)) return null;
            var contents = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Config>(contents);
        }
    }
}