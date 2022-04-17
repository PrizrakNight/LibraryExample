using System;
using System.Collections.Generic;

namespace LibraryExample.Domain.Entities
{
    // Доменная сущность стеллажа.
    // Кратко о дизайне: Доменные сущности не хранят в себе детали хранения в разных источниках данных.
    // К таким деталям относятся внешние ключи и прочие метаданные не интересные бизнесу, но необходимые для связей Sql или NoSql.
    public class Rack
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
        {
            return $"Номер стеллажа: {Number}";
        }
    }
}
