using Microsoft.Data.Sqlite;

namespace PrjAndmebaas
{
    internal class Andmebaas
    {
        private readonly string _connectionString;

        public Andmebaas()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarkvara.db");
            _connectionString = $"Data Source={dbPath}";
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}
