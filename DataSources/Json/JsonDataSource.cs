using LibraryExample.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryExample.DataSources.Json
{
    public class JsonDataSource : FileDataSource
    {
        protected override Tuple<Book[], Author[], Rack[]> ReadModels(FileStream fileStream)
        {
            using (var reader = new StreamReader(fileStream))
            {
                var jsonString = reader.ReadToEnd();
                var jsonRoot = JsonConvert.DeserializeObject<JsonRoot>(jsonString);

                PopulateRelatedEntities(jsonRoot);

                return Tuple.Create(
                    jsonRoot.Books.Cast<Book>().ToArray(),
                    jsonRoot.Authors.Cast<Author>().ToArray(),
                    jsonRoot.Racks.Cast<Rack>().ToArray());
            }
        }

        protected override void WriteModels(FileStream fileStream)
        {
            using (var writer = new StreamWriter(fileStream))
            {
                // Здесь нет проверки на существования авторов в JsonRoot.Authors, если в Book.Author указан автор которого нет в JsonRoot.Authors.
                // Это может порушить консистентность данных
                // Данные проверки опущены в целях экономии времени.
                var jsonRoot = new JsonRoot
                {
                    Books = GetJsonBooks(),
                    Authors = GetJsonAuthors(),
                    Racks = GetJsonRacks(),
                };

                var jsonString = JsonConvert.SerializeObject(jsonRoot, Formatting.Indented);

                writer.Write(jsonString);
            }
        }

        private void PopulateRelatedEntities(JsonRoot jsonRoot)
        {
            // Тут достаточно сложные и неоптимизированные алгоритмы заполнения связанных сущностей.
            // Выбрано решение "в лоб" ради экономии времени.
            foreach (var jsonBook in jsonRoot.Books)
            {
                jsonBook.Author = jsonRoot.Authors.First(x => x.Id == jsonBook.AuthorId);
                jsonBook.Rack = jsonRoot.Racks.First(x => x.Id == jsonBook.RackId);
            }

            foreach (var jsonAuthor in jsonRoot.Authors)
            {
                jsonAuthor.Books = jsonRoot.Books.Where(x => x.AuthorId == jsonAuthor.Id).Cast<Book>().ToList();
            }

            foreach (var jsonRack in jsonRoot.Racks)
            {
                jsonRack.Books = jsonRoot.Books.Where(x => x.RackId == jsonRack.Id).Cast<Book>().ToList();
            }
        }

        private List<JsonRack> GetJsonRacks()
        {
            return Racks.Select(x =>
            {
                var jsonRack = new JsonRack
                {
                    Id = x.Id,
                    Number = x.Number,
                    Books = null
                };

                if (x.Books != null && x.Books.Count > 0)
                    jsonRack.BookIds = x.Books.Select(book => book.Id).ToList();

                return jsonRack;
            }).ToList();
        }

        private List<JsonBook> GetJsonBooks()
        {
            return Books.Select(x =>
            {
                return new JsonBook
                {
                    Id = x.Id,
                    Name = x.Name,
                    Author = null,
                    Rack = null,
                    RackId = x.Rack.Id,
                    AuthorId = x.Author.Id,
                };
            }).ToList();
        }

        private List<JsonAuthor> GetJsonAuthors()
        {
            return Authors.Select(x =>
            {
                var jsonAuthor = new JsonAuthor
                {
                    Id = x.Id,
                    Name = x.Name,
                    Books = null,
                    BookIds = new List<Guid>()
                };

                if (x.Books != null && x.Books.Count > 0)
                    jsonAuthor.BookIds = x.Books.Select(book => book.Id).ToList();

                return jsonAuthor;
            }).ToList();
        }
    }
}
