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

        public List<Tarkvaranõuded> LeiaSobivadTarkvarad(IRiistvara riistvara, int kategooriaId)
        {
            List<Tarkvaranõuded> nõuded = new List<Tarkvaranõuded>();

            using SqliteConnection connection = _database.GetConnection();
            connection.Open();

            string query = @"
                           SELECT Id, Nimi, MinOS, MinRAM, MinKettamaht
                           FROM Tarkvara
                           WHERE KategooriaId = @kategooriaId
                           AND MinOS <= @os
                           AND MinRAM <= @ram
                           AND MinKettamaht <= @disk
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@kategooriaId", kategooriaId);
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

        public List<Kriteeriumid> LoeKriteeriumidKategooriaJargi(int kategooriaId)
        {
            List<Kriteeriumid> kriteeriumid = new List<Kriteeriumid>();

            using SqliteConnection connection = _database.GetConnection();
            connection.Open();

            string query = @"
                           SELECT Id, Nimi, KategooriaId
                           FROM Kriteerium
                           WHERE KategooriaId = @kategooriaId
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@kategooriaId", kategooriaId);

            using SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Kriteeriumid kriteerium = new Kriteeriumid
                {
                    Id = reader.GetInt32(0),
                    Nimi = reader.GetString(1),
                    KategooriaId = reader.GetInt32(2)
                };

                kriteeriumid.Add(kriteerium);
            }

            return kriteeriumid;
        }

        public List<int> LoeTarkvaraKriteeriumid(int tarkvaraId)
        {
            List<int> kriteeriumid = new List<int>();

            using SqliteConnection connection = _database.GetConnection();
            connection.Open();

            string query = @"
                           SELECT KriteeriumId
                           FROM TarkvaraKriteerium
                           WHERE TarkvaraId = @tarkvaraId
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@tarkvaraId", tarkvaraId);

            using SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                kriteeriumid.Add(reader.GetInt32(0));
            }

            return kriteeriumid;
        }

    }
}
