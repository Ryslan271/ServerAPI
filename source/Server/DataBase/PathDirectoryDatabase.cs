namespace Server.DataBase
{
    public partial class DatabaseContext
    {
        // TODO: ...
        public static string PathBeforeProjectDirectory = null!; // путь до основной папки с базами данных

        public static string ProjectDirectory = null!; // путь до папки проекта


        /// <summary>
        /// Определение путей до рабочей базы
        /// </summary>
        /// <param name="id">Принимает номер клиента</param>
        /// <param name="nameDb">Принимает название новой базы данных</param>
        public static string PathBeforeDataBase(string id, string nameDb)
        {
            FindingPathToMainFolder(id);
            ValidateAndCreationDirectoryForClient();

            return Path.Combine(PathBeforeProjectDirectory, $"{nameDb}.db");
        }

        /// <summary>
        /// Вывод всех файлов в нужной директории
        /// </summary>
        /// <param name="idClient">Номер клиента</param>
        /// <returns>Список всех названий файлов</returns>
        public static string[] NamesDirectoryForClient(string idClient)
        {
            FindingPathToMainFolder(idClient);


            return Directory.GetFiles(ValidateAndCreationDirectoryForClient().ToString());
        }

        /// <summary>
        /// Проверка директории на существование и последующее её создание 
        /// </summary>
        private static DirectoryInfo ValidateAndCreationDirectoryForClient()
        {
            DirectoryInfo newdirInfo = new(PathBeforeProjectDirectory);

            if (newdirInfo.Exists == false)
                newdirInfo.Create();

            return newdirInfo;
        }

        /// <summary>
        /// Создание пути до рабочей папки клиента  
        /// </summary>
        /// <param name="idClient">Номер клиента</param>
        private static void FindingPathToMainFolder(string idClient) =>
            PathBeforeProjectDirectory =
                Path.Combine(string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split("\\")[0..^4]), $"DataBase\\Model\\{idClient}");
    }
}
