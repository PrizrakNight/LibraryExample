using LibraryExample.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LibraryExample.DataSources
{
    public static class InMemoryDataSourceEx
    {
        public static bool ContainsData(this IInMemoryDataSource inMemoryDataSource)
        {
            var countOfData = inMemoryDataSource.Books.Count + inMemoryDataSource.Racks.Count + inMemoryDataSource.Authors.Count;

            return countOfData > 0;
        }

        public static TSource SeedTestData<TSource>(this TSource inMemoryDataSource)
            where TSource : IInMemoryDataSource
        {
            var authors = new List<Author>();
            var books = new List<Book>();
            var racks = new List<Rack>();

            authors.AddRange(new Author[]
            {
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Adriano Giudice"
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Lulu Corlang"
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "John Smith"
                }
            });

            racks.AddRange(new Rack[]
            {
                new Rack
                {
                    Id = Guid.NewGuid(),
                    Number = 1
                },
                new Rack
                {
                    Id = Guid.NewGuid(),
                    Number = 2
                },
                new Rack
                {
                    Id = Guid.NewGuid(),
                    Number = 3
                }
            });

            books.AddRange(new Book[]
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    Name = "Day to night",
                    Author = authors[0],
                    Rack = racks[0],
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Name = "Story about a boy",
                    Author = authors[1],
                    Rack = racks[0],
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Name = "Love affairs",
                    Author = authors[2],
                    Rack = racks[1],
                }
            });

            inMemoryDataSource.Authors.AddRange(authors);
            inMemoryDataSource.Books.AddRange(books);
            inMemoryDataSource.Racks.AddRange(racks);

            return inMemoryDataSource;
        }
    }
}
