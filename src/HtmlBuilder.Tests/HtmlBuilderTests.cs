using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{

    [TestClass]
    public class HtmlBuilderTests
    {
        private const string _div = "div";
        private const string _span = "span";
        private const string _class = "test-class";
        private const string _cssClass = ".test-class";

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HtmlBuilder_NoSelectorPassed_ArgumentException()
        {
            var html = new HtmlBuilder();
            html.ToString(); // throws exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HtmlBuilder_EmptySelectorPassed_ArgumentException()
        {
            var html = new HtmlBuilder(string.Empty);
            html.ToString(); // throws exception
        }

        [TestMethod]
        public void HtmlBuilder_TagNamePassed_RendersTag()
        {
            var html = new HtmlBuilder(_div);

            Assert.AreEqual("<div></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_ClassPassed_RendersDivWithClass()
        {
            var html = new HtmlBuilder(_cssClass);

            Assert.AreEqual("<div class=\"" + _class + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_TagNameClass_RendersTagWithClass()
        {
            var html = new HtmlBuilder(_span + _cssClass);

            Assert.AreEqual("<" + _span + " class=\"" + _class + "\"></" + _span + ">", html.ToString());
        }
    }
}
