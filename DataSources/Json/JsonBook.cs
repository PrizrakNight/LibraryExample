using LibraryExample.Domain.Entities;
using Newtonsoft.Json;
using System;

namespace LibraryExample.DataSources.Json
{
    public class JsonBook : Book
    {
        public Guid AuthorId { get; set; }
        public Guid RackId { get; set; }

        [JsonIgnore]
        public override Author Author { get; set; }

        [JsonIgnore]
        public override Rack Rack { get; set; }
    }
}
