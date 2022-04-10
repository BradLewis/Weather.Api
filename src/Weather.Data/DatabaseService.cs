using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Weather.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            var host = configuration["host"];
            var user = configuration["username"];
            var password = configuration["password"];
            var database = configuration["Database"];
            _connectionString = $"server={host};user={user};password={password};database={database};";
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}