using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{

    [TestClass]
    public class HtmlBuilderTests
    {
        private const string _div = "div";
        private const string _span = "span";
        private const string _id = "unique";
        private const string _id2 = "fail";
        private const string _class = "test-class";
        private const string _class2 = "test-class2";
        private const string _cssClass = ".test-class";
        private const string _cssClass2 = ".test-class2";
        private const string _attr = "test";
        private const string _attrValue = "true";

        [TestInitialize]
        public void TestInitialize()
        {

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
        public void HtmlBuilder_IdPassed_RendersDivWithId()
        {
            var html = new HtmlBuilder("#" + _id);

            Assert.AreEqual("<div id=\"" + _id + "\"></div>", html.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HtmlBuilder_MultipleIds_ArgumentException()
        {
            var html = new HtmlBuilder("#" + _id + " #" + _id2);
            html.ToString();
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

        [TestMethod]
        public void HtmlBuilder_MultipleClasses_RendersDivWithMultipleClasses()
        {
            var html = new HtmlBuilder(_cssClass + " " + _cssClass2);

            // Tag builder reverses the classes...
            Assert.AreEqual("<div class=\"" + _class2 + " " + _class + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AttributeWithNoValue_RendersTheAttributeName()
        {
            var html = new HtmlBuilder("[" + _attr + "]");

            Assert.AreEqual("<div " + _attr + "=\"\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AttributeWithValue_RendersAttribute()
        {
            var html = new HtmlBuilder("[" + _attr + "='" + _attrValue + "']");

            Assert.AreEqual("<div " + _attr + "=\"" + _attrValue + "\"></div>", html.ToString());
        }
    }
}

