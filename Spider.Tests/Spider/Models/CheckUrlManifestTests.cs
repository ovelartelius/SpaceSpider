using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spider.Extensions;
using Spider.Models;

namespace Spider.Tests.Spider.Models
{
    [TestClass]
    public class CheckUrlManifestTests
    {
        [TestMethod]
        public void UseUserAgent_False_Test()
        {
            //Arrange
            var manifest = new CheckUrlManifest();
            manifest.UserAgent = string.Empty;

            //Act
            var result = manifest.UseUserAgent;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UseUserAgent_True_Test()
        {
            //Arrange
            var manifest = new CheckUrlManifest();
            manifest.UserAgent = "SpaceSpider";

            //Act
            var result = manifest.UseUserAgent;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UseProxy_False_Test()
        {
            //Arrange
            var manifest = new CheckUrlManifest();
            manifest.ProxyAddress = string.Empty;

            //Act
            var result = manifest.UseProxy;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UseProxy_True_Test()
        {
            //Arrange
            var manifest = new CheckUrlManifest();
            manifest.ProxyAddress = "https://gia.sebank.se:8080/";

            //Act
            var result = manifest.UseProxy;

            //Assert
            Assert.IsTrue(result);
        }

    }
}
