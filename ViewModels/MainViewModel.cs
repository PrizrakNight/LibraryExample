using LibraryExample.Commands;
using System.Collections.ObjectModel;

namespace LibraryExample.ViewModels
{
    /// <summary>
    /// ViewModel для главного экрана.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        // Свойства
        public ObservableCollection<BookViewModel> Books
        {
            get => _books;
            set
            {
                _books = value;

                OnPropertyChanged();
            }
        }

        public BookViewModel SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;

                OnPropertyChanged();
            }
        }

        // Команды
        public OpenBookEditorCommand EditBookCommand { get; }
        public OpenBookEditorCommand AddNewBookCommand { get; }
        public OpenFileCommand OpenFileCommand { get; }
        public SaveToDatabaseCommand SaveToDatabaseCommand { get; }
        public DeleteBookCommand DeleteBookCommand { get; }
        public SaveToFileCommand SaveToFileCommand { get; }
        public ExitCommand ExitCommand { get; }

        // Приватные поля
        private BookViewModel _selectedBook;
        private ObservableCollection<BookViewModel> _books = new ObservableCollection<BookViewModel>();

        public MainViewModel()
        {
            EditBookCommand = new OpenBookEditorCommand(this, true);
            AddNewBookCommand = new OpenBookEditorCommand(this, false);
            OpenFileCommand = new OpenFileCommand(this);
            DeleteBookCommand = new DeleteBookCommand(this);
            SaveToFileCommand = new SaveToFileCommand();
            SaveToDatabaseCommand = new SaveToDatabaseCommand();
            ExitCommand = new ExitCommand();
        }
    }
}
