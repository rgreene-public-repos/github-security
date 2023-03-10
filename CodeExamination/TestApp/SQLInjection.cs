using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TestApp
{
    internal class SQLInjection
    {

        const string connectionString = "Data Source=:memory:";
        SQLiteConnection _con = new SQLiteConnection(connectionString);

        public SQLInjection() {
            _con.Open();
        }

        public string? Init()
        {
            string stm = "SELECT SQLITE_VERSION()";
            using var cmd = new SQLiteCommand(stm, _con);
            string? version = cmd.ExecuteScalar()!.ToString();
            return version;
        }


        public void CreateTable()
        {
            using var cmd = new SQLiteCommand(_con);
            cmd.CommandText = "DROP TABLE IF EXISTS cars";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
            name TEXT, price INT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
            cmd.ExecuteNonQuery();

            return;
        }

        public List<string>? QueryTable(string filterValue)
        {
            List<string> result = new List<string>();
            string stm = $"SELECT * FROM cars where name like '%{filterValue}%' LIMIT 5";
            using var cmd = new SQLiteCommand(stm, _con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                result.Add($"{rdr.GetInt32(0)}-{rdr.GetString(1)}");
            }
            return result;
        }

    }
}
