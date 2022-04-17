using LibraryExample.DataSources;
using LibraryExample.DataSources.Adapters;
using LibraryExample.DataSources.EntityFramework;
using LibraryExample.DataSources.Json;
using LibraryExample.Factories;
using System.IO;
using System.Windows;

namespace LibraryExample
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IInMemoryToSqlAdapter InMemoryToSqlAdapter
        {
            get
            {
                if (_inMemoryToSqlAdapter == null)
                    _inMemoryToSqlAdapter = new InMemoryToEF6Adapter(new AppDbContext(ConnectionStrings.LocalhostSqlServer));

                return _inMemoryToSqlAdapter;
            }
        }

        // Фабрика обязана создавать такие типы книг, которые находятся в IFileDataSource.
        // Если типы будут отличаться, то это приведет к проблемам при сохранении данных в БД с использованием EF.
        // Исключение будет следующее: InvalidCastException.
        public static IBookFactory BookFactory { get; private set; }
        public static IFileDataSource FileDataSource { get; private set; }

        private const string _initialFilename = "InitialLibraryData.json";

        private static IInMemoryToSqlAdapter _inMemoryToSqlAdapter;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (FileDataSource == null)
                FileDataSource = new JsonDataSource();

            if (BookFactory == null)
                BookFactory = new JsonBookFactory();

            if (!File.Exists(_initialFilename))
            {
                var jsonDataSource = new JsonDataSource();

                jsonDataSource.SeedTestData().SaveToFile(_initialFilename);
            }
        }
    }
}
