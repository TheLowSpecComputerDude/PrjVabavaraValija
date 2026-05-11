using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjAndmebaas
{
    public class LoeAndmed : ILoeAndmed
    {
        private readonly Andmebaas _andmed;

        public LoeAndmed()
        {
            _andmed = new Andmebaas();
        }

        List<Tarkvaranõuded> ILoeAndmed.LeiaSobivadTarkvarad(double osVersioon, double ram, double vabaRuum, int kategooriaId)
        {
            List<Tarkvaranõuded> nõuded = new List<Tarkvaranõuded>();

            using SqliteConnection ühendus = _andmed.LooÜhendus();
            ühendus.Open();

            string query = @"
                           SELECT Id, Nimi, MinOS, MinRAM, MinKettamaht
                           FROM Tarkvara
                           WHERE KategooriaId = @kategooriaId
                           AND MinOS <= @os
                           AND MinRAM <= @ram
                           AND MinKettamaht <= @disk
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, ühendus);
            cmd.Parameters.AddWithValue("@kategooriaId", kategooriaId);
            cmd.Parameters.AddWithValue("@os", osVersioon);
            cmd.Parameters.AddWithValue("@ram", ram);
            cmd.Parameters.AddWithValue("@disk", vabaRuum);
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

        List<Kriteeriumid> ILoeAndmed.LoeKriteeriumidKategooriaJargi(int kategooriaId)
        {
            List<Kriteeriumid> kriteeriumid = new List<Kriteeriumid>();

            using SqliteConnection ühendus = _andmed.LooÜhendus();
            ühendus.Open();

            string query = @"
                           SELECT Id, Nimi, Kirjeldus, KategooriaId
                           FROM Kriteerium
                           WHERE KategooriaId = @kategooriaId
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, ühendus);
            cmd.Parameters.AddWithValue("@kategooriaId", kategooriaId);

            using SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Kriteeriumid kriteerium = new Kriteeriumid
                {
                    Id = reader.GetInt32(0),
                    Nimi = reader.GetString(1),
                    Kirjeldus = reader.GetString(2),
                    KategooriaId = reader.GetInt32(3)
                };

                kriteeriumid.Add(kriteerium);
            }

            return kriteeriumid;
        }

        List<int> ILoeAndmed.LoeTarkvaraKriteeriumid(int tarkvaraId)
        {
            List<int> kriteeriumid = new List<int>();

            using SqliteConnection ühendus = _andmed.LooÜhendus();
            ühendus.Open();

            string query = @"
                           SELECT KriteeriumId
                           FROM TarkvaraKriteerium
                           WHERE TarkvaraId = @tarkvaraId
                           ";

            using SqliteCommand cmd = new SqliteCommand(query, ühendus);
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
