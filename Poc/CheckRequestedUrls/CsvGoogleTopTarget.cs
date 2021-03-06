﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRequestedUrls
{
    public class CsvGoogleTopTarget
    {
        public string Url { get; set; }
        public int IncomingLinks { get; set; }
        public int WebsitesWithLink { get; set; }

        public static CsvGoogleTopTarget FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            var item = new CsvGoogleTopTarget();
            item.Url = Convert.ToString(values[0]);
            item.IncomingLinks = Convert.ToInt32(values[1]);
            item.WebsitesWithLink = Convert.ToInt32(values[2]);
            return item;
        }
    }
}
