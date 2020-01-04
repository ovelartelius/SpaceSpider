using System;

namespace CheckUrls.Models.Csv
{
    public class CsvSimpleUrl
    {
        public string Url { get; set; }

        public static CsvSimpleUrl FromCsv(string csvLine, string separator = ",")
        {
            string[] values = csvLine.Split(char.Parse(separator));
            var item = new CsvSimpleUrl();
            item.Url = Convert.ToString(values[0]);
            return item;
        }
    }
}
