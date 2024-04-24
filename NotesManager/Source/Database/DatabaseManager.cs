using Microsoft.Data.Sqlite;

namespace NotesManager.Source.Database
{
    public class DatabaseManager
    {
        // Строка подключения к базе данных 
        private const string CONNECTION_STRING = "Data Source=Database.sqlite;";

        // Возвращает новое подключение к базе данных
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(CONNECTION_STRING);
        }

        // Инициализирует базу данных
        public static void InitializeDatabase()
        {
            // Получение подключения к базе данных
            using (var connection = GetConnection())
            {
                // Открытие подключения
                connection.Open();

                // запрос для создания таблицы, если она ещё не существует
                string tableCreationQuery = @"
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY,
                    Title TEXT NOT NULL,
                    Content TEXT NOT NULL
                );";


                // Выполнение запроса на создание таблицы
                using (var command = new SqliteCommand(tableCreationQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
