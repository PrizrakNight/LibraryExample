using LibraryExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace LibraryExample.DataSources
{
    public abstract class FileDataSource : IFileDataSource
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Rack> Racks { get; set; } = new List<Rack>();

        public void LoadFromFile(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (File.Exists(fileName) == false)
                return;

            try
            {
                var models = ReadModels(File.OpenRead(fileName));

                Books = new List<Book>(models.Item1);
                Authors = new List<Author>(models.Item2);
                Racks = new List<Rack>(models.Item3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveToFile(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            var directoryName = Path.GetDirectoryName(fileName);

            // Если fileName содержит название папки и этой папки нет, то ее нужно создать
            if (string.IsNullOrWhiteSpace(directoryName) == false && Directory.Exists(directoryName) == false)
                Directory.CreateDirectory(directoryName);

            var fileStream = File.Create(fileName);

            WriteModels(fileStream);
        }

        // Намеренный хардкод Tuple<Book[], Author[], Rack[]> с целью экономии времени.
        protected abstract Tuple<Book[], Author[], Rack[]> ReadModels(FileStream fileStream);
        protected abstract void WriteModels(FileStream fileStream);
    }
}
