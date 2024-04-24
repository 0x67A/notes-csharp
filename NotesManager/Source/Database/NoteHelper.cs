using Microsoft.Data.Sqlite;
using NotesManager.Source.Models;

namespace NotesManager.Source.Database
{
    public class NoteHelper
    {
        // Добавляет новую заметку в базу данных
        public void Add(Note note)
        {
            // Создаем соединение с базой данных
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand("INSERT INTO Notes (Title, Content) VALUES (@Title, @Content)", connection))
                {
                    command.Parameters.AddWithValue("@Title", note.Title);
                    command.Parameters.AddWithValue("@Content", note.Content);
                    command.ExecuteNonQuery(); // Выполняем команду
                }
            }
        }

        // Получает все заметки из базы данных
        public List<Note> GetAll()
        {
            List<Note> notes = new List<Note>(); // Список для хранения заметок
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT Id, Title, Content FROM Notes", connection))
                {
                    using (var reader = command.ExecuteReader()) // Читаем данные из базы
                    {
                        while (reader.Read()) // Перебираем строки в ответе от базы
                        {
                            notes.Add(new Note // Добавляем заметки в список
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Content = reader["Content"].ToString()
                            });
                        }
                    }
                }
            }
            return notes; // Возвращаем список заметок
        }

        // Удаляет заметку по идентификатору
        public bool Delete(int id)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open(); // Открываем соединение
                using (var command = new SqliteCommand("DELETE FROM Notes WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int affectedRows = command.ExecuteNonQuery();
                    return affectedRows > 0;
                }
            }
        }

        // Ищет заметку по ключевому слову
        public List<Note> SearchNotes(string keyword)
        {
            List<Note> notes = new List<Note>(); // Список для хранения заметок
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT Id, Title, Content FROM Notes WHERE Title LIKE @Keyword OR Content LIKE @Keyword", connection))
                {
                    command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                    using (var reader = command.ExecuteReader()) // получаем результаты
                    {
                        // Перебираем все строки
                        while (reader.Read())
                        {
                            notes.Add(new Note
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Content = reader["Content"].ToString()
                            });
                        }
                    }
                }
            }
            return notes;
        }
    }
}
