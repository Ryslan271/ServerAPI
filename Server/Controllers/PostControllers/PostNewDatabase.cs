using Microsoft.AspNetCore.Mvc;
using Server.DataBase;
using System.Data.SQLite;


namespace Server.Controllers.PostControllers
{
    /// <summary>
    /// Создание новой базы данных
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PostNewDatabase : ControllerBase
    {
        /// <summary>
        /// Создание новой базы
        /// </summary>
        /// <param name="Id">Номер клиента</param>
        /// <param name="NameDB">Название базы</param>
        /// <returns>Состояние</returns>
        [HttpPost]
        [Route("PostNameDB")]
        public string PostNameDB(int id, string NameDB)
        {
            try
            {
                string dbFilePath = DatabaseContext.PathBeforeDataBase(id, NameDB); // создание пути для бд

                SQLiteConnection.CreateFile(dbFilePath); // создание бд

                return $"База данных {NameDB} успешно создана";
            }
            catch (Exception mes)
            {
                return mes.Message;
            }
        }
    }
}
