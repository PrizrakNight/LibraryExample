using LibraryExample.DataSources;
using Microsoft.Win32;
using System.Windows;

namespace LibraryExample.Commands
{
    public class SaveToFileCommand : CommandBase
    {
        private readonly string _filter;

        public SaveToFileCommand(string filter = "Json files (*.json) | *.json")
        {
            _filter = filter;
        }

        public override bool CanExecute(object parameter)
        {
            return App.FileDataSource.ContainsData();
        }

        public override void Execute(object parameter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "*.json",
                Title = "Выберите файл для сохранения",
                Filter = _filter
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                App.FileDataSource.SaveToFile(saveFileDialog.FileName);

                MessageBox.Show("Данные успешно сохранены!", "Сохранение данных");
            }
        }
    }
}
