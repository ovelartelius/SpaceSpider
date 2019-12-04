using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRequestedUrls
{
    public class CsvGoogleCrawlError
    {
        public string Url { get; set; }
        public int StatusCode { get; set; }
        public string ErrorInGoogleNews { get; set; }
        public DateTime? ErrorFound { get; set; }
        public string Category { get; set; }
        public string Platform { get; set; }
        public DateTime? LastChecked { get; set; }

        public static CsvGoogleCrawlError FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            CsvGoogleCrawlError item = new CsvGoogleCrawlError();
            item.Url = Convert.ToString(values[0]);
            item.StatusCode = Convert.ToInt32(values[1]);
            item.ErrorInGoogleNews = Convert.ToString(values[2]);
            item.ErrorFound = Convert.ToDateTime(values[3]);
            item.Category = Convert.ToString(values[4]);
            item.Platform = Convert.ToString(values[5]);
            item.LastChecked = Convert.ToDateTime(values[6]);
            return item;
        }
    }
}
