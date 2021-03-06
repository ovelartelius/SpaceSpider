﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CheckRequestedUrls
{
    public class SpiderPageLink
    {
        public SpiderPageLink()
        {
            Headers = new NameValueCollection();
        }

        public string Url { get; set; }

        public Uri Uri
        {
            get
            {
                Uri uri = null;

                try
                {
                    uri = new Uri(Url);
                }
                catch
                {
                    uri = null;
                }

                return uri;
            }
        }

        public string StatusCodeJson { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Description { get; set; }

        public bool Erroneous { get; set; }

        [JsonIgnore]
        public NameValueCollection Headers { get; set; }

        public string Content { get; set; }

        public long Time { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }

        public string Server { get; set; }

        public int HistoricHits { get; set; }

        public bool Ignored { get; set; }
    }
}
