using Microsoft.Data.Sqlite;

namespace PrjAndmebaas
{
    internal class Andmebaas
    {
        private readonly string _ühendusString;

        public Andmebaas()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarkvara.db");
            _ühendusString = $"Data Source={dbPath}";
        }

        public SqliteConnection LooÜhendus()
        {
            return new SqliteConnection(_ühendusString);
        }
    }
}
