using LibraryExample.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibraryExample.DataSources.Json
{
    public class JsonRack : Rack
    {
        public List<Guid> BookIds { get; set; }

        [JsonIgnore]
        public override ICollection<Book> Books { get; set; }
    }
}
