using System;
using Spider.Models;

namespace LinkjuiceCreator
{
    public class CsvMappedUrls
    {
        public string SourceUrl { get; set; }

        public string DestinationUrl { get; set; }

        public CheckUrlResult PageResult { get; set; }
        public int PageId { get; set; }
        public string ErrorMessage { get; set; }

        public static CsvMappedUrls FromCsv(string csvLine, string seperator = ",")
        {
            string[] values = csvLine.Split(char.Parse(seperator));
            var item = new CsvMappedUrls();
            item.SourceUrl = Convert.ToString(values[0]);
            item.DestinationUrl = Convert.ToString(values[1]);
            return item;
        }
    }
}
