namespace LibraryExample.DataSources
{
    /// <summary>
    /// Источник данных: файл.
    /// </summary>
    public interface IFileDataSource : IInMemoryDataSource
    {
        void SaveToFile(string fileName);

        /// <summary>
        /// Загружает все данные из файла в память.
        /// </summary>
        void LoadFromFile(string fileName);
    }
}
