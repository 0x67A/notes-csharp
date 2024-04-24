using NotesManager.Source.Services;

namespace NotesManager.Source.UI
{
    public class ConsoleMenu
    {
        private readonly NoteService _noteService; // сервис для работы с заметками

        // конструктор класса, инициализирует сервис
        public ConsoleMenu(NoteService noteService)
        {
            _noteService = noteService;
        }

        // отображает основное меню и обрабатывает пользовательский ввод
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Добавить заметку\n2. Показать заметки\n3. Удалить заметку\n4. Найти заметку по ключевому слову\n5. Выйти\n");
                Console.Write("Ваш ответ: ");
                string? option = Console.ReadLine();
                Console.Clear();
                switch (option)
                {
                    case "1":
                        AddNoteInteraction();
                        break;
                    case "2":
                        DisplayNotes();
                        break;
                    case "3":
                        DeleteNoteInteraction();
                        break;
                    case "4":
                        SearchNotesInteraction();
                        break;
                    case "5":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный ввод, попробуйте еще раз.");
                        break;
                }
            }
        }

        // добавление новой заметки
        private void AddNoteInteraction()
        {
            Console.WriteLine("Введите заголовок:");
            string? title = Console.ReadLine();
            Console.WriteLine("Введите содержание:");
            string? content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Заголовок или содержание не могут быть пустыми.");
                return;
            }

            _noteService.AddNote(title, content);
            Console.WriteLine("Заметка успешно добавлена.");
        }

        // отображает все заметки
        private void DisplayNotes()
        {
            var notes = _noteService.GetAllNotes();
            if (notes.Count == 0)
            {
                Console.WriteLine("Заметки отсутствуют.");
                return;
            }

            foreach (var note in notes)
            {
                Console.WriteLine($"ID: {note.Id}, Заголовок: {note.Title ?? "Без заголовка"}, Содержание: {note.Content ?? "Без содержания"}");
            }
            Console.WriteLine();
        }

        // удаление заметки
        private void DeleteNoteInteraction()
        {
            Console.WriteLine("Введите ID заметки для удаления:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_noteService.DeleteNote(id))
                {
                    Console.WriteLine("Заметка успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Заметка не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите корректное число.");
            }
        }

        // поиск заметки по слову
        private void SearchNotesInteraction()
        {
            Console.WriteLine("Введите ключевое слово для поиска:");
            string? keyword = Console.ReadLine();
            var notes = _noteService.SearchNotes(keyword!);
            if (notes.Count == 0)
            {
                Console.WriteLine("Нет заметок, содержащих указанное ключевое слово.");
                return;
            }
            foreach (var note in notes)
            {
                Console.WriteLine($"ID: {note.Id}, Заголовок: {note.Title}, Содержание: {note.Content}");
            }
        }
    }
}
