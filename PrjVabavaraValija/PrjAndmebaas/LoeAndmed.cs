using Microsoft.Data.Sqlite;
using PrjRiistvara;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjAndmebaas
{
    public class LoeAndmed : ILoeAndmed
    {
        private readonly Andmebaas _database;

        public LoeAndmed()
        {
            _database = new Andmebaas();
        }

        public List<Tarkvaranõuded> LeiaSobivadTarkvarad(IRiistvara riistvara)
        {
            List<Tarkvaranõuded> nõuded = new List<Tarkvaranõuded>();

            using SqliteConnection connection = _database.GetConnection();
            connection.Open();

            string query = @"
                           SELECT Id, Nimi, MinOS, MinRAM, MinKettamaht
                           FROM Tarkvara
                           WHERE MinOS <= @os
                           AND MinRAM <= @ram
                           AND MinKettamaht <= @disk
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@os", riistvara.OSVersion);
            cmd.Parameters.AddWithValue("@ram", riistvara.RAM);
            cmd.Parameters.AddWithValue("@disk", riistvara.FreeSpace);
            using SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Tarkvaranõuded minNõuded = new Tarkvaranõuded
                {
                    Id = reader.GetInt32(0),
                    Nimi = reader.GetString(1),
                    MinOS = reader.GetInt32(2),
                    MinRAM = reader.GetDouble(3),
                    MinKettamaht = reader.GetDouble(4)
                };

                nõuded.Add(minNõuded);
            }
            return nõuded;
        }
            
    }
}
