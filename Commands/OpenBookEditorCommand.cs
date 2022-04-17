using LibraryExample.DataSources;
using LibraryExample.ViewModels;
using LibraryExample.Views;

namespace LibraryExample.Commands
{
    public class OpenBookEditorCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly bool _isEditMode;

        public OpenBookEditorCommand(MainViewModel mainViewModel, bool isEditMode)
        {
            _mainViewModel = mainViewModel;
            _isEditMode = isEditMode;
        }

        public override bool CanExecute(object parameter)
        {
            if (_isEditMode)
                return _mainViewModel.SelectedBook != null;

            return App.FileDataSource.ContainsData();
        }

        public override void Execute(object parameter)
        {
            var bookEditorViewModel = new BookEditorViewModel(_isEditMode ? _mainViewModel.SelectedBook : null);
            var bookEditor = new BookEditorView
            {
                DataContext = bookEditorViewModel
            };

            if (bookEditor.ShowDialog() == true)
            {
                bookEditorViewModel.ApplyChanges();

                if (_isEditMode)
                    return;

                _mainViewModel.Books.Add(bookEditorViewModel.EditableBook);

                App.FileDataSource.Books.Add(bookEditorViewModel.EditableBook.Book);
            }
        }
    }
}
