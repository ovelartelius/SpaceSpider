using System;

namespace Spider.Models.Csv
{
    public class CsvSimpleUrl
    {
        public string Url { get; set; }

        public static CsvSimpleUrl FromCsv(string csvLine, string separator = ",")
        {
	        var item = new CsvSimpleUrl();
            if (!string.IsNullOrEmpty(separator))
	        {
		        string[] values = csvLine.Split(char.Parse(separator));
		        item.Url = Convert.ToString(values[0]);

            }
            else
            {
	            item.Url = csvLine;
            }
            return item;
        }
    }
}
