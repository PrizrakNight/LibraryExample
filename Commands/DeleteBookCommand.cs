using LibraryExample.ViewModels;

namespace LibraryExample.Commands
{
    public class DeleteBookCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public DeleteBookCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return _mainViewModel.SelectedBook != null;
        }

        public override void Execute(object parameter)
        {
            var foundBook = App.FileDataSource.Books.Find(x => x.Id == _mainViewModel.SelectedBook.Book.Id);

            if (foundBook != null)
            {
                App.FileDataSource.Books.Remove(foundBook);

                _mainViewModel.Books.Remove(_mainViewModel.SelectedBook);
                _mainViewModel.SelectedBook = null;
            }
        }
    }
}
