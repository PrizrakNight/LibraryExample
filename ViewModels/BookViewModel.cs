using LibraryExample.Domain.Entities;

namespace LibraryExample.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        public Book Book { get; }

        public string NormalizeName => ToString();

        public string Name
        {
            get => Book.Name;
            set
            {
                Book.Name = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(NormalizeName));
            }
        }

        public Author Author
        {
            get => Book.Author;
            set
            {
                Book.Author = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(NormalizeName));
            }
        }

        public Rack Rack
        {
            get => Book.Rack;
            set
            {
                Book.Rack = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(NormalizeName));
            }
        }

        public BookViewModel(Book book)
        {
            Book = book;
        }

        public override string ToString()
        {
            return $"Книга: {Book.Name}, Автор: {Book.Author.Name}, Стеллаж: {Book.Rack.Number}";
        }
    }
}
