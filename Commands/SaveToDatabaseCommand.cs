using LibraryExample.DataSources;
using System;
using System.Windows;

namespace LibraryExample.Commands
{
    public class SaveToDatabaseCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return App.FileDataSource.ContainsData();
        }

        public override async void Execute(object parameter)
        {
            try
            {
                // Здесь также не хватает ProgressBar для отображения прогресса выполнения переноса данных из памяти в БД.
                // Не сделано с целью экономии времени.
                await App.InMemoryToSqlAdapter.AdaptToSqlAsync(App.FileDataSource);

                MessageBox.Show("Сохранение прошло успешно!", "Сохранение в БД");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при сохранении данных в БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
