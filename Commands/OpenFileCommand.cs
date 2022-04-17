using LibraryExample.ViewModels;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryExample.Commands
{
    public class OpenFileCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        private readonly string _filter;

        public OpenFileCommand(MainViewModel mainViewModel, string filter = "Json files (*.json) | *.json")
        {
            _mainViewModel = mainViewModel;
            _filter = filter;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = "*.json",
                Filter = _filter,
                Title = "Выберите файл с данными",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                App.FileDataSource.LoadFromFile(openFileDialog.FileName);

                _mainViewModel.Books = new ObservableCollection<BookViewModel>(App.FileDataSource.Books.Select(x => new BookViewModel(x)));
                _mainViewModel.SelectedBook = null;
            }
        }
    }
}
