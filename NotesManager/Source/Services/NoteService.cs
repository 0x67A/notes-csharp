using NotesManager.Source.Database;
using NotesManager.Source.Models;

namespace NotesManager.Source.Services
{
    public class NoteService
    {
        private readonly NoteHelper _helper = new NoteHelper();

        public void AddNote(string title, string content) // Добавление заметки
        {
            _helper.Add(new Note { Title = title, Content = content });
        }
        
        // Получение всех заметок
        public List<Note> GetAllNotes() => _helper.GetAll();

        // Удаление заметки
        public bool DeleteNote(int id) => _helper.Delete(id);
        
        // Поиск заметки по ключевому слову
        public List<Note> SearchNotes(string keyword) => _helper.SearchNotes(keyword);

    }
}
