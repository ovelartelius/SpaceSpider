using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spider.Extensions;
using Spider.Models;

namespace Spider.Tests.Spider
{
    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void GetAllAnchors_NoMatch()
        {
            //Arrange
            var content = "<html><body><a>Fake link</a></body></html>";

            //Act
            var linkList = HtmlParser.GetAllAnchors(content);
            
            //Assert
            Assert.IsNotNull(linkList);
            Assert.AreEqual(0, linkList.Count);
        }

        [TestMethod]
        public void GetAllAnchors_OneMatch()
        {
            //Arrange
            var content = "<html><body><a href=\"http://myfakedomain.com/\">Fake link</a></body></html>";

            //Act
            var linkList = HtmlParser.GetAllAnchors(content);

            //Assert
            Assert.IsNotNull(linkList);
            Assert.AreEqual(1, linkList.Count);
            Assert.AreEqual("http://myfakedomain.com/", linkList[0]);
        }

        [TestMethod]
        public void GetAllAnchors_SebNoPrivatLinksMatch()
        {
            //Arrange
            var checkUrlResult = Json.LoadJson<CheckUrlResult>("httpssebnoprivat.json");
            var content = checkUrlResult.Content;

            //Act
            var linkList = HtmlParser.GetAllAnchors(content);

            //Assert
            Assert.IsNotNull(linkList);
            Assert.AreEqual(49, linkList.Count);
            Assert.AreEqual("http://browsehappy.com/", linkList[0]);
            Assert.AreEqual("http://www.google.com/chromeframe/?redirect=true", linkList[1]);
            Assert.AreEqual("#primarycontent", linkList[2]);
            Assert.AreEqual("#main-navigation", linkList[3]);
            Assert.AreEqual("#login-links-list", linkList[4]);
            Assert.AreEqual("https://nettbank.seb.no", linkList[5]);
            Assert.AreEqual("https://investor.vps.no/vit/servlet/no.vps.investorclient.servlets.LogonServlet?avtalehaver=11270", linkList[6]);
            Assert.AreEqual("http://tradingstation.seb.se", linkList[7]);
            Assert.AreEqual("https://otf.seb.se", linkList[8]);
            Assert.AreEqual("https://cfi.mb.seb.se/pqq_portal/sebflow/startpage", linkList[9]);
            Assert.AreEqual("https://id.seb.se/ccs", linkList[10]);
            Assert.AreEqual("https://ts.seb.se/publicweb/research/#/research", linkList[11]);
            Assert.AreEqual("http://researchonline.sebgroup.com", linkList[12]);
            Assert.AreEqual("https://factoring.mb.seb.se/aqs/", linkList[13]);
            Assert.AreEqual("https://secure.sebkort.com/nis/m/ssno/external/t/login/index", linkList[14]);
            Assert.AreEqual("#mobile-navigation", linkList[15]);
            Assert.AreEqual("/", linkList[16]);
            Assert.AreEqual("/", linkList[17]);
            Assert.AreEqual("/bedrifter-og-institusjoner", linkList[18]);
            Assert.AreEqual("/privat", linkList[19]);
            Assert.AreEqual("/om-seb/om-oss", linkList[20]);
            Assert.AreEqual("/privat/family-office-group", linkList[21]);
            Assert.AreEqual("/", linkList[22]);
            Assert.AreEqual("https://nettbank.seb.no/Account/Login", linkList[23]);
            Assert.AreEqual("https://secure.sebkort.com/nis/m/ssno/external/t/login/index", linkList[24]);
            Assert.AreEqual("mailto:familyoffice@seb.no", linkList[25]);
            Assert.AreEqual("http://lt.morningstar.com/9oj5cr67q1/fundquickrank/default.aspx", linkList[26]);
            Assert.AreEqual("/privat/family-office-group", linkList[27]);
            Assert.AreEqual("/om-seb/om-oss/kundeprofil", linkList[28]);
            Assert.AreEqual("/site-assistance/faq-lei", linkList[29]);
            Assert.AreEqual("/privat/family-office-group/investeringstjenester", linkList[30]);
            Assert.AreEqual("/privat/family-office-group/finansiering", linkList[31]);
            Assert.AreEqual("/privat/family-office-group/stiftelser-og-legater", linkList[32]);
            Assert.AreEqual("/privat/family-office-group/family-advisory", linkList[33]);
            Assert.AreEqual("/", linkList[34]);
            Assert.AreEqual("/om-seb/alle-telefonnumre-og-adresser", linkList[35]);
            Assert.AreEqual("/om-seb/presse-och-nyheter/pressekontakt", linkList[36]);
            Assert.AreEqual("https://lt.morningstar.com/9oj5cr67q1/screener/a/default.aspx", linkList[37]);
            Assert.AreEqual("https://seb.se/pow/wave/apps/iban/ibancalc.asp", linkList[38]);
            Assert.AreEqual("/om-seb/valutakurser", linkList[39]);
            Assert.AreEqual("https://developer.sebgroup.com/", linkList[40]);
            Assert.AreEqual("https://developer.luxhub.com/", linkList[41]);
            Assert.AreEqual("/site-assistance/sitemap", linkList[42]);
            Assert.AreEqual("/site-assistance/settings", linkList[43]);
            Assert.AreEqual("/site-assistance/sikkerhet-pa-nett", linkList[44]);
            Assert.AreEqual("/site-assistance/priser-og-vilkaar", linkList[45]);
            Assert.AreEqual("/site-assistance/personvern", linkList[46]);
            Assert.AreEqual("http://www.sebgroup.com", linkList[47]);
            Assert.AreEqual("#page27853", linkList[48]);
        }
    }
}
