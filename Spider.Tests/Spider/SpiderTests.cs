using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spider.Extensions;

namespace Spider.Tests.Spider
{
    [TestClass]
    public class SpiderTests
    {
        [TestMethod]
        public void ShouldUrlBeIgnored_NoMatch()
        {
            //Arrange
            var newUrl = "http://mysite/blah";
            var ignorePatterns = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
            var list = ignorePatterns.SplitToList();
            var spider = new global::Spider.Spider();

            //Act
            var result = spider.ShouldUrlBeIgnored(newUrl, list);
            
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ShouldUrlBeIgnored_Match()
        {
            //Arrange
            var newUrl = "http://mysite/pow/blah";
            var ignorePatterns = "^.*pow.*$\r\n^.*\\/powershell\\/.*$";
            var list = ignorePatterns.SplitToList();
            var spider = new global::Spider.Spider();

            //Act
            var result = spider.ShouldUrlBeIgnored(newUrl, list);

            //Assert
            Assert.IsTrue(result);
        }

    }
}
