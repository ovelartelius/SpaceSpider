using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spider.Extensions;

namespace Spider.Tests.Spider
{
    [TestClass]
    public class RobotsTxtTests
    {
        //CreateRobotsTxtUrl
        [TestMethod]
        public void CreateRobotsTxtUrl_already()
        {
	        //Arrange
	        var siteUrl = "http://seb.no/robots.txt";
	        var robotsTxt = new global::Spider.RobotsTxt();

	        //Act
	        var result = robotsTxt.CreateRobotsTxtUrl(siteUrl);

	        //Assert
	        Assert.AreEqual(siteUrl, result);
        }

        [TestMethod]
        public void CreateRobotsTxtUrl_withoutslash()
        {
	        //Arrange
	        var siteUrl = "http://seb.no";
	        var robotsTxt = new global::Spider.RobotsTxt();

	        //Act
	        var result = robotsTxt.CreateRobotsTxtUrl(siteUrl);

	        //Assert
	        Assert.AreEqual($"{siteUrl}/robots.txt", result);
        }

        [TestMethod]
        public void CreateRobotsTxtUrl_withslash()
        {
	        //Arrange
	        var siteUrl = "http://seb.no/";
	        var robotsTxt = new global::Spider.RobotsTxt();

	        //Act
	        var result = robotsTxt.CreateRobotsTxtUrl(siteUrl);

	        //Assert
	        Assert.AreEqual($"{siteUrl}robots.txt", result);
        }
    }
}
