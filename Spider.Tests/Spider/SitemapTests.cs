using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spider.Tests.Spider
{
    [TestClass]
    public class SitemapTests
    {
        [TestMethod]
        public void FindSitemapInSebNoRobotsTxt_Match()
        {
            //Arrange
            var robotsTxtContent = File.ReadAllText("robots-sebno.txt");

            //Act
            var result = Sitemap.ParseSitemapUrl(robotsTxtContent);
            
            //Assert
            Assert.AreEqual("https://seb.se/MogulSeoManagerSitemap.aspx?https=true", result);
        }
    }
}
