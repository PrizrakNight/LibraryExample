
using System.Collections.Generic;

namespace LibraryExample.DataSources.Json
{
    public sealed class JsonRoot
    {
        public List<JsonAuthor> Authors { get; set; }
        public List<JsonBook> Books { get; set; }
        public List<JsonRack> Racks { get; set; }
    }
}
