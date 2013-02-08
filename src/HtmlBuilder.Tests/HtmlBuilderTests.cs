using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{

    [TestClass]
    public class HtmlBuilderTests
    {
        [TestMethod]
        public void HtmlBuilder_NoSelectorPassed_RendersEmptyString()
        {
            var html = new HtmlBuilder();

            Assert.AreEqual(string.Empty, html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_TagNamePassed_RendersTag()
        {
            var html = new HtmlBuilder("div");

            Assert.AreEqual("<div></div>", html.ToString());
        }
    }
}
