using System.Collections.Generic;
using LinkjuiceCreator.Models;
using Spider.Models;

namespace LinkjuiceCreator
{
    public class Manifest
    {
        public LinkjuiceCreatorSettings Settings { get; set; }

        public List<CsvMappedUrls> MappedUrls { get; set; }

        public List<CheckUrlResult> PageResults { get; set; }
    }
}
