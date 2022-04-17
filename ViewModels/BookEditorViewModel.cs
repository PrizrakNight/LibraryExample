using LibraryExample.Domain.Entities;

namespace LibraryExample.ViewModels
{
    public class BookEditorViewModel : ViewModelBase
    {
        public BookViewModel EditableBook { get; }

        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;

                OnPropertyChanged();
            }
        }

        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;

                OnPropertyChanged();
            }
        }

        public Rack SelectedRack
        {
            get => _selectedRack;
            set
            {
                _selectedRack = value;

                OnPropertyChanged();
            }
        }

        public Author[] AllAuthors => _authors;
        public Rack[] AllRacks => _racks;

        private Rack _selectedRack;
        private Author _selectedAuthor;

        private Author[] _authors;
        private Rack[] _racks;

        private string _bookName;

        public BookEditorViewModel(BookViewModel bookViewModel = null)
        {
            _authors = App.FileDataSource.Authors.ToArray();
            _racks = App.FileDataSource.Racks.ToArray();

            if (bookViewModel == null)
            {
                // Используем новый экземпляр книги.
                // Тип экземпляра определяет класс App.
                // Если работать с EF, то типы книг не должны быть разные, иначе будет выброшено искючение: InvalidCastException. 
                var newBook = App.BookFactory.CreateNew();

                bookViewModel = new BookViewModel(newBook);

                _selectedAuthor = _authors[0];
                _selectedRack = _racks[0];
                _bookName = newBook.Name;
            }
            else
            {
                _selectedAuthor = bookViewModel.Author;
                _selectedRack = bookViewModel.Rack;
                _bookName = bookViewModel.Name;
            }

            EditableBook = bookViewModel;
        }

        /// <summary>
        /// Применяет изменения к <see cref="EditableBook"/>
        /// </summary>
        public void ApplyChanges()
        {
            EditableBook.Name = BookName;
            EditableBook.Author = SelectedAuthor;
            EditableBook.Rack = SelectedRack;
        }
    }
}
