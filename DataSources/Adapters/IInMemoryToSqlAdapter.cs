using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryExample.DataSources.Adapters
{
    /// <summary>
    /// Адаптер предназначенный для переноса данных из памяти в реляционнную базу данных.
    /// <para>Источником правды всегда является <see cref="IInMemoryDataSource"/></para>
    /// </summary>
    public interface IInMemoryToSqlAdapter : IDisposable
    {
        Task<int> AdaptToSqlAsync(IInMemoryDataSource dataSource,
            CancellationToken cancellationToken = default);
    }
}
