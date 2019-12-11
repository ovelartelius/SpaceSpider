using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spider.Extensions;

namespace Spider.Tests
{
    [TestClass]
    public class StringExtensions
    {
        [TestMethod]
        public void ToFileSafeName()
        {
            //Arange
            var list = new List<NameValue>();
            list.Add(new NameValue { Uri = "http://seb.se", Result = "httpsebse" });
            list.Add(new NameValue { Uri = "http://seb.se/", Result = "httpsebse" });
            list.Add(new NameValue { Uri = "http://seb.se/sitemap.json", Result = "httpsebsesitemapjson" });

            foreach (var item in list)
            {
                //Act
                var safeName = item.Uri.ToFileSafeName();

                //Assert
                Assert.AreEqual(item.Result, safeName);
            }
        }

        public class NameValue
        {
            public string Uri { get; set; }
            public string Result { get; set; }
        }
    }
}
