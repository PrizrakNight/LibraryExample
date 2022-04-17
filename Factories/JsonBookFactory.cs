using LibraryExample.DataSources.Json;
using LibraryExample.Domain.Entities;
using System;

namespace LibraryExample.Factories
{
    public class JsonBookFactory : IBookFactory
    {
        public Book CreateNew()
        {
            return new JsonBook
            {
                Id = Guid.NewGuid(),
                Name = "Новая книга"
            };
        }
    }
}
