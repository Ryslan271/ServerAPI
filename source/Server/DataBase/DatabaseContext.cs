using Microsoft.EntityFrameworkCore;
using Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net.Sockets;
using System.Xml;
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
        /// <returns>Новую строку подключения без открытым соединением SQLiteConnection</returns>
        public static SQLiteConnection ConnectionDB(string id, string Name)
        {

            string connectionString = $"Data Source={PathBeforeDataBase(id, Name)};Version=3;";
            SQLiteConnection m_dbConnection = new(connectionString);

            return m_dbConnection;
        }

        /// <summary>
        /// Регистрация нового клиента
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Login"></param>
        /// <param name="Password"></param>
        /// <returns>int user ID </returns>
        //public static int UserRegistration(string Username, string Login, string Password)
        //{

        //}

        /// <summary>
        /// Создание основной базы данных всех клиентов
        /// </summary>
        /// <returns>bool</returns>
        public static (string?, bool)  CreatingMainDatabase()
        {
            try
            {
                if (ValidateExistenceFile(Сonstants.FULL_PATH_MAIN_DATABASE))
                    throw new PathException("Ошибка основной базы данных 'MainBase'", false);

                SQLiteConnection.CreateFile(Сonstants.FULL_PATH_MAIN_DATABASE); // создание бд

                string sqlCommand = $"Create Table {Сonstants.NAME_MAIN_DATABASE} " +
                    $"([Id] [int] IDENTITY(1,1) NOT NULL," +
                    $" [Username] [nvarchar](50) NOT NULL,)" +
                    $" [Login] [nvarchar](50) NOT NULL,)" +
                    $" [Password] [nvarchar](50) NOT NULL)";

                SQLiteConnection m_dbConnection = DatabaseContext.ConnectionDB(Сonstants.PATH_MAIN_DATABASE, "MainBase"); // подключение к нужной базе

                m_dbConnection.Open(); // открытие соедение

                SQLiteCommand command = new(sqlCommand, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                return (null, true);
            }
            catch (PathException ex) 
            {
                return (ex.Message, ex.state);
            }
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="Path">Путь до файла</param>
        /// <returns>True|False</returns>
        public static bool ValidateExistenceFile(string Path) =>
            File.Exists(Path);

        /// <summary>
        /// ....
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="NameTable"></param>
        /// <returns>List<string> все атрибуты из таблицы</returns>
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
