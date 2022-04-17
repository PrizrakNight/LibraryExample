using LibraryExample.DataSources.Adapters;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryExample.DataSources.EntityFramework
{
    public class InMemoryToEF6Adapter : IInMemoryToSqlAdapter
    {
        private readonly AppDbContext _context;

        public InMemoryToEF6Adapter(AppDbContext context)
        {
            _context = context;
        }

        // Логика метода проста:
        // Источником правды является IInMemoryDataSource.
        // Если в источнике правды есть элементы которых нету в AppDbContext, значит эти элементы следует добавить в AppDbContext.
        // Если в источнике правды нет элементов которые есть в AppDbContext, значит эти элементы надо удалить из AppDbContext.
        // Все остальные данные будут обновлены.
        public async Task<int> AdaptToSqlAsync(IInMemoryDataSource dataSource,
            CancellationToken cancellationToken = default)
        {
            // Поскольку нас не интересует нагруженность по памяти, мы просто стягиваем все данные из БД в память приложения.
            var authorsFromDb = await _context.Authors.ToListAsync(cancellationToken);
            var booksFromDb = await _context.Books.ToListAsync(cancellationToken);
            var racksFromDb = await _context.Racks.ToListAsync(cancellationToken);

            // Удаление сущностей из БД, которых нету в источнике правды.
            // Как можно заметить здесь не учитываются связи между сущностями, а значит можно удалить автора, на которого ссылается книга или же стеллаж.
            // Учет связей опущен в целях экономии времени, будет выброшено исключение согласно ограничениям на стороне БД.
            _context.Authors.RemoveRange(authorsFromDb.Where(x => dataSource.Authors.Any(author => author.Id == x.Id) == false));
            _context.Books.RemoveRange(booksFromDb.Where(x => dataSource.Books.Any(book => book.Id == x.Id) == false));
            _context.Racks.RemoveRange(racksFromDb.Where(x => dataSource.Racks.Any(rack => rack.Id == x.Id) == false));

            // Добавление или обновление сущностей которые либо есть в БД, либо их нет.
            _context.Authors.AddOrUpdate(dataSource.Authors.ToArray());

            // AddOrUpdate обновляет только свойства главного объекта, это не отражается на внешние ключи связанных сущностей.
            // Мне показалось обидным остутствие такой возможности, пришлось плодить этот стремный код внизу.
            // Если есть вариант нормального обновления связей, без внесения деталей хранения в доменную сущность, то я рад об этом услышать.
            foreach (var book in dataSource.Books)
            {
                var foundBook = booksFromDb.FirstOrDefault(x => x.Id == book.Id);
                var foundAuthor = authorsFromDb.FirstOrDefault(x => x.Id == book.Author.Id);
                var foundRack = racksFromDb.FirstOrDefault(x => x.Id == book.Rack.Id);

                if (foundBook != null)
                {
                    foundBook.Name = book.Name;
                    foundBook.Author = foundAuthor ?? book.Author;
                    foundBook.Rack = foundRack ?? book.Rack;

                    continue;
                }

                book.Author = foundAuthor ?? book.Author;
                book.Rack = foundRack ?? book.Rack;

                _context.Books.Add(book);
            }

            _context.Racks.AddOrUpdate(dataSource.Racks.ToArray());

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
