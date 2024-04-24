using NotesManager.Source.Database;
using NotesManager.Source.Services;
using NotesManager.Source.UI;

class Program
{
    static void Main(string[] args)
    {
        DatabaseManager.InitializeDatabase(); // Инициализация базы данных

        NoteService noteService = new NoteService(); // Создание сервиса для работы с заметками
        ConsoleMenu menu = new ConsoleMenu(noteService); // Создание меню 
        menu.ShowMenu(); // Отображение меню 
    }
}
