using Microsoft.AspNetCore.Mvc;
using Server.DataBase;
using System.Data.SQLite;

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
        public string Post(int id, string nameDB, string nameTable)
        {
            try
            {
                SQLiteConnection m_dbConnection = DatabaseContext.ConnectionDB(id, nameDB); // подключение к нужной базе

                m_dbConnection.Open(); // открытие соедение


                string sql = $"Create Table {nameTable} (name varchar(20), score int)";

                SQLiteCommand command = new(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                return "Все прошло успешно";
            }
            catch (Exception mes)
            {
                return mes.Message;
            }
        }
    }
}
