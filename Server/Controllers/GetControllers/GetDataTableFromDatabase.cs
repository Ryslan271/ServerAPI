using Microsoft.AspNetCore.Mvc;
using Server.DataBase;
using System.Data.SQLite;


namespace Server.Controllers.GetControllers
{
    /// <summary>
    /// Контроллер для вывода данных
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GetDataTableFromDatabase : ControllerBase
    {
        /// <summary>
        /// Вывод данных из нужных таблиц
        /// </summary>
        /// <param name="id">Номер клиента</param>
        /// <param name="NameDb">Название базы данных</param>
        /// <param name="NameTb">Название таблицы</param>
        /// <returns>Данные из таблицы</returns>
        [HttpGet]
        [Route("GetDataTableFromDatabase")]
        public string Get(int id, string NameDb, string NameTb)
        {
            string connectionString = $"Data Source={DatabaseContext.PathBeforeDataBase(id, NameDb)};Version=3;";
            SQLiteConnection m_dbConnection = new(connectionString);

            return string.Join("\n", DatabaseContext.ReadData(m_dbConnection, NameTb));
        }
    }
}
