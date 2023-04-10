using Microsoft.AspNetCore.Mvc;
using Server.DataBase;
using System.Data.SQLite;
using System.Linq;

namespace Server.Controllers.GetControllers
{
    /// <summary>
    /// Контроллер для вывода всех таблиц клиента
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GetAllDatabaseClient : ControllerBase
    {
        /// <summary>
        /// Вывод всех баз данных клиента
        /// </summary>
        /// <param name="Id">Номер клиента</param>
        /// <returns>Данные из таблицы</returns>
        [HttpGet]
        [Route("GetAllDatabaseFromDirectoryClient")]
        public string Get(int Id)
        {
            try
            {
                return string.Join("", string.Join("\n", DatabaseContext.NamesDirectoryForClient(Id)).Split("\\")[^0..^2]);
            }
            catch
            {
                return "Что то пошло не так";
            }
        }
    }
}
