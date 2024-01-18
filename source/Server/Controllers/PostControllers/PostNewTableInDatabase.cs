using Microsoft.AspNetCore.Mvc;
using Server.DataBase;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Server.Controllers.PostControllers
{
    /// <summary>
    /// Создание новой таблицы в нужной базе
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PostNewTableInDatabase : ControllerBase
    {
        /// <summary>
        /// Создание новой таблицы в указанной базе данных
        /// </summary>
        /// <param name="id">Номер клиента</param>
        /// <param name="nameDB">Название базы</param>
        /// <param name="nameTable">Название таблицы</param>
        /// <returns>Состояние</returns>
        [HttpPost]
        [Route("PostNameTableInDB")]
        public string Post(int id, string nameDB, string nameTable, (string attributes, string ))
        {
            // name1 nvarchar(20)| name2 nvarchar(20)

            string[][] dictionary = attributes.Split('|').Select(column => Regex.Split(column.Trim(), @"[ ]+")).ToArray();

            string sqlCommand = $"Create Table {nameTable} (\n{string.Join(",\n", dictionary.Select(pair => $"{pair[0]} {pair[1]}"))}\n)";

            try
            {
                SQLiteConnection m_dbConnection = DatabaseContext.ConnectionDB(id, nameDB); // подключение к нужной базе

                m_dbConnection.Open(); // открытие соедение

                SQLiteCommand command = new(sqlCommand, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                return sqlCommand;
            }
            catch (Exception mes)
            {
                return mes.Message;
            }
        }
    }
}
