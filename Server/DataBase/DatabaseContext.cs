using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SQLite;
using static Azure.Core.HttpHeader;

namespace Server.DataBase
{
    public partial class DatabaseContext : DbContext
    {
        public static DatabaseContext Entities { get; private set; }

        public static void InitializeEntities() => Entities = new();

        /// <summary>
        /// Создание соединение с запрашиваемой базой данных
        /// </summary>
        /// <param name="id">Номер клиента</param>
        /// <param name="Name">Название нужной базы</param>
        /// <returns>Новую страку поключения без открытым соединением SQLiteConnection</returns>
        public static SQLiteConnection ConnectionDB(int id, string Name)
        {

            string connectionString = $"Data Source={PathBeforeDataBase(id, Name)};Version=3;";
            SQLiteConnection m_dbConnection = new(connectionString);

            return m_dbConnection;
        }

        public static List<string> ReadData(SQLiteConnection conn, string NameTable)
        {
            List<string> readText = new();

            conn.Open();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM {NameTable}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                readText.Add(myreader);
            }
            conn.Close();

            return readText;
        }
    }
}
