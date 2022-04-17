using System;
using System.Collections.Generic;

namespace LibraryExample.Domain.Entities
{
    // Доменная сущность автора.
    // Кратко о дизайне: Доменные сущности не хранят в себе детали хранения в разных источниках данных.
    // К таким деталям относятся внешние ключи и прочие метаданные не интересные бизнесу, но необходимые для связей Sql или NoSql.
    public class Author
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
        {
            return $"Автор: {Name}";
        }
    }
}
