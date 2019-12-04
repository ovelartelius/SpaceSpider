using System;

namespace CheckRequestedUrls
{
    public class CsvSimpleUrl
    {
        public string Url { get; set; }

        public static CsvSimpleUrl FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            var item = new CsvSimpleUrl();
            item.Url = Convert.ToString(values[0]);
            return item;
        }
    }
}
