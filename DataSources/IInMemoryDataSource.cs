using LibraryExample.Domain.Entities;
using System.Collections.Generic;

namespace LibraryExample.DataSources
{
    public interface IInMemoryDataSource
    {
        // Дабы не усложнять логику приложения,
        // было принято решение собрать в этом интерфейсе все сущности.
        List<Book> Books { get; set; }
        List<Author> Authors { get; set; }
        List<Rack> Racks { get; set; }
    }
}
