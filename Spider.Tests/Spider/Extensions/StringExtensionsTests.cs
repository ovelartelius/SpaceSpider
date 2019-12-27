using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spider.Extensions;

namespace Spider.Tests.Spider.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ToFileSafeName_Test()
        {
            //Arrange
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

        [TestMethod]
        public void SplitToList_Test()
        {
            //Arrange
            var data = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";

            //Act
            var list = data.SplitToList();
            
            //Assert
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("^.*pow.*$", list[0]);
            Assert.AreEqual(@"^.*\/powershell\/.*$", list[1]);
        }

        [TestMethod]
        public void SplitToList_Empty_Test()
        {
            //Arrange
            var data = string.Empty;

            //Act
            var list = data.SplitToList();

            //Assert
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void SwapHostname_SimpleAddress_httpTohttps()
        {
            //Arrange
            var oldUrl = "http://oldcrapsite.com/mypage";
            var newHostname = "https://seb.no/";

            //Act
            var newUrl = oldUrl.SwapHostname(newHostname);

            //Assert
            Assert.AreEqual("https://seb.no/mypage", newUrl);
        }

        [TestMethod]
        public void SwapHostname_SimpleAddress_UsingPortOnOld()
        {
            //Arrange
            var oldUrl = "http://oldcrapsite.com:8080/mypage";
            var newHostname = "https://seb.no/";

            //Act
            var newUrl = oldUrl.SwapHostname(newHostname);

            //Assert
            Assert.AreEqual("https://seb.no/mypage", newUrl);
        }

        [TestMethod]
        public void SwapHostname_SimpleAddress_UsingPortOnNew()
        {
            //Arrange
            var oldUrl = "http://oldcrapsite.com/mypage";
            var newHostname = "https://seb.no:8080/";

            //Act
            var newUrl = oldUrl.SwapHostname(newHostname);

            //Assert
            Assert.AreEqual("https://seb.no:8080/mypage", newUrl);
        }

        [TestMethod]
        public void SwapHostname_SimpleAddress_WithQueryStringOnOld()
        {
            //Arrange
            var oldUrl = "http://oldcrapsite.com/mypage?myquery=value";
            var newHostname = "https://seb.no/";

            //Act
            var newUrl = oldUrl.SwapHostname(newHostname);

            //Assert
            Assert.AreEqual("https://seb.no/mypage?myquery=value", newUrl);
        }

        [TestMethod]
        public void SwapHostname_SimpleAddress_WithAnchorOnOld()
        {
            //Arrange
            var oldUrl = "http://oldcrapsite.com/mypage#myanchor";
            var newHostname = "https://seb.no/";

            //Act
            var newUrl = oldUrl.SwapHostname(newHostname);

            //Assert
            Assert.AreEqual("https://seb.no/mypage", newUrl);
        }

        [TestMethod]
        public void SwapHostname_SimpleAddress_WithUrlEncodeOnOld()
        {
            //Arrange
            var oldUrl = "http://oldcrapsite.com/my%20page";
            var newHostname = "https://seb.no/";

            //Act
            var newUrl = oldUrl.SwapHostname(newHostname);

            //Assert
            Assert.AreEqual("https://seb.no/my%20page", newUrl);
        }

        public class NameValue
        {
            public string Uri { get; set; }
            public string Result { get; set; }
        }
    }
}
