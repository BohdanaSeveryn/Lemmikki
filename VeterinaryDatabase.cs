using System;
using Microsoft.Data.Sqlite;
using Lemmikki.Models;

namespace Lemmikki
{
    public class VeterinaryDatabase
    {
        private string connectionString = "Data Source=veterinaryDatabase.db";

        public VeterinaryDatabase()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var pragmaCommand = connection.CreateCommand();
            pragmaCommand.CommandText = "PRAGMA foreign_keys = ON;";
            pragmaCommand.ExecuteNonQuery();

            var commandForOwnerCreating = connection.CreateCommand();
            commandForOwnerCreating.CommandText = @"
                CREATE TABLE IF NOT EXISTS Owner (
                    id INTEGER PRIMARY KEY,
                    nimi TEXT,
                    osoite TEXT,
                    puhelinnumero TEXT
                );
            ";
            
            commandForOwnerCreating.ExecuteNonQuery();

            var commandForPetCreating = connection.CreateCommand();
            commandForPetCreating.CommandText = @"
                CREATE TABLE IF NOT EXISTS Pet (
                    id INTEGER PRIMARY KEY,
                    nimi TEXT,
                    omistaja_id INTEGER,
                    tyyppi TEXT,
                    FOREIGN KEY (omistaja_id) REFERENCES Owner(id)
                );
            ";
            commandForPetCreating.ExecuteNonQuery();

            connection.Close();
        }

        public void LisaaOwner(string nimi, string osoite, string puhelinnumero)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Owner (nimi, osoite, puhelinnumero)
                VALUES (@Nimi, @Osoite, @Puhelinnumero);
            ";
            command.Parameters.AddWithValue("@Nimi", nimi);
            command.Parameters.AddWithValue("@Osoite", osoite);
            command.Parameters.AddWithValue("@Puhelinnumero", puhelinnumero);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int OwnerIdSearching(string nimi, string osoite, string puhelinnumero)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = @"
                SELECT id FROM Owner
                WHERE nimi = $nimi AND osoite = $osoite AND puhelinnumero = $puhelinnumero;
            ";
            checkCommand.Parameters.AddWithValue("$nimi", nimi);
            checkCommand.Parameters.AddWithValue("$osoite", osoite);
            checkCommand.Parameters.AddWithValue("$puhelinnumero", puhelinnumero);

            using var reader = checkCommand.ExecuteReader();
            int omistajaId;

            if (reader.Read())
            {
                omistajaId = reader.GetInt32(0);
            }
            else
            {
                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO Owner (nimi, osoite, puhelinnumero)
                    VALUES ($nimi, $osoite, $puhelinnumero);
                ";
                insertCommand.Parameters.AddWithValue("$nimi", nimi);
                insertCommand.Parameters.AddWithValue("$osoite", osoite);
                insertCommand.Parameters.AddWithValue("$puhelinnumero", puhelinnumero);
                insertCommand.ExecuteNonQuery();

                var getIdCommand = connection.CreateCommand();
                getIdCommand.CommandText = "SELECT last_insert_rowid();";
                omistajaId = Convert.ToInt32(getIdCommand.ExecuteScalar());
            }

            connection.Close();
            return omistajaId;
        }

        public string SearchingNumber(string nameFromUser)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT Owner.puhelinnumero
            FROM Owner
            LEFT JOIN Pet
            ON Owner.id = Pet.omistaja_id
            WHERE Pet.nimi = $name;
            ";
            command.Parameters.AddWithValue("name", nameFromUser);
            using var reader = command.ExecuteReader();
            string numero = "Tälle eläimelle ei ole omistajaa tietokannassa.";
            if (reader.Read())
            {
                numero = reader.GetString(0);
            }

            connection.Close();
            return numero;
        }


        public void LisaaPet(string nimi, int omistaja_id, string tyyppi)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Pet (nimi, omistaja_id, tyyppi)
                VALUES (@Nimi, @Omistaja_id, @Tyyppi);
            ";
            command.Parameters.AddWithValue("@Nimi", nimi);
            command.Parameters.AddWithValue("@Omistaja_id", omistaja_id);
            command.Parameters.AddWithValue("@Tyyppi", tyyppi);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
