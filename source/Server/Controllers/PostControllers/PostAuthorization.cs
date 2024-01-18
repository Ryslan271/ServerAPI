using Microsoft.AspNetCore.Mvc;
using Server.DataBase;
using Server.Exceptions;
using System.Data.SQLite;

namespace Server.Controllers.PostControllers
{
    /// <summary>
    /// Создание новой базы данных
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PostAuthorization
    {
        /// <summary>
        /// Авторизация клиента
        /// </summary>
        /// <param name="Username">Имя</param>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <returns>(Id клиента, либо сообщение об ошибке, состояние корректного авторизирования)</returns>
        [HttpPost]
        [Route("PostNameDB")]
        public (string, bool) Authorization(string Username, string Login, string Password)
        {
            try
            {
                if (DatabaseContext.ValidateExistenceFile(Сonstants.FULL_PATH_MAIN_DATABASE) == false)
                    DatabaseContext.CreatingMainDatabase();



            }
            catch (PathException ex)
            {
                return (ex.Message, ex.state);
            }
        }
    }
}
