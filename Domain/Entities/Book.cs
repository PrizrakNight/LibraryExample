using System;

namespace LibraryExample.Domain.Entities
{
    // Доменная сущность книги.
    // Кратко о дизайне: Доменные сущности не хранят в себе детали хранения в разных источниках данных.
    // К таким деталям относятся внешние ключи и прочие метаданные не интересные бизнесу, но необходимые для связей Sql или NoSql.
    public class Book
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual Author Author { get; set; }
        public virtual Rack Rack { get; set; }
    }
}
