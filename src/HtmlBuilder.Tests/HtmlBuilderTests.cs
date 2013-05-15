using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    /// <summary>
    /// Integration tests......
    /// </summary>
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


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HtmlBuilder_WithEmptySelectorPassed_ShouldThrowArgumentException()
        {
            var html = new HtmlBuilder(string.Empty);
            html.ToString(); // throws exception
        }

        [TestMethod]
        public void HtmlBuilder_WithTagNamePassed_ShouldRenderTag()
        {
            var html = new HtmlBuilder(_div);

            Assert.AreEqual("<div></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithSelfClosingTag_ShouldRenderSelfClosingTag()
        {
            var html = new HtmlBuilder("input");

            Assert.AreEqual("<input />", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithIdPassed_ShouldRenderDivWithId()
        {
            var html = new HtmlBuilder("#" + _id);

            Assert.AreEqual("<div id=\"" + _id + "\"></div>", html.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HtmlBuilder_WithMultipleIds_ShouldThrowArgumentException()
        {
            var html = new HtmlBuilder("#" + _id + "#" + _id2);
            html.ToString();
        }

        [TestMethod]
        public void HtmlBuilder_WithClassPassed_ShouldRenderDivWithClass()
        {
            var html = new HtmlBuilder(_cssClass);

            Assert.AreEqual("<div class=\"" + _class + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithTagNameClass_ShouldRenderTagWithClass()
        {
            var html = new HtmlBuilder(_span + _cssClass);

            Assert.AreEqual("<" + _span + " class=\"" + _class + "\"></" + _span + ">", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithMultipleClasses_ShouldRenderDivWithMultipleClasses()
        {
            var html = new HtmlBuilder(_cssClass + _cssClass2);

            // Tag builder reverses the classes...
            Assert.AreEqual("<div class=\"" + _class2 + " " + _class + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithAttributeWithNoValue_ShouldRenderDivWithTheAttributeName()
        {
            var html = new HtmlBuilder("[" + _attr + "]");

            Assert.AreEqual("<div " + _attr + "=\"\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithAttributeWithValue_ShouldRenderDivWithAttribute()
        {
            var html = new HtmlBuilder("[" + _attr + "='" + _attrValue + "']");

            Assert.AreEqual("<div " + _attr + "=\"" + _attrValue + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithSiblingSelector_ShouldRenderTwoTopLevelElements()
        {
            var html = new HtmlBuilder("p + a");

            Assert.AreEqual("<p></p><a></a>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithChildSelector_ShouldRenderElementWithChild()
        {
            var html = new HtmlBuilder("ul > li");

            Assert.AreEqual("<ul><li></li></ul>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithMultipleSelector_ShouldRenderMultipleTags()
        {
            var html = new HtmlBuilder("div*2");

            Assert.AreEqual("<div></div><div></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithComplexSelector_ShouldRenderCorrectly()
        {
            var html = new HtmlBuilder("ul#test > li.items * 3 > a[href=\"/SomeURL\"]");

            Assert.AreEqual("<ul id=\"test\">" +
                "<li class=\"items\"><a href=\"/SomeURL\"></a></li>" +
                "<li class=\"items\"><a href=\"/SomeURL\"></a></li>" +
                "<li class=\"items\"><a href=\"/SomeURL\"></a></li>" +
            "</ul>", html.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HtmlBuilder_AddID_OnMultipleElements_ShouldThrowArgumentException()
        {
            new HtmlBuilder("div*2").AddID("isUnique");
        }

        [TestMethod]
        public void HtmlBuilder_AddID_ShouldAddAnIDToTheElement()
        {
            const string id = "isUnique";
            var html = new HtmlBuilder("div").AddID(id);

            Assert.AreEqual("<div id=\"" + id + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AddCSSClass_ShouldAddClassToElement()
        {
            var html = new HtmlBuilder("div").AddCSSClass("testing");

            Assert.AreEqual("<div class=\"testing\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AddAttribute_ShouldAddAttributeToElement()
        {
            const string key = "href";
            const string value = "/Home";
            var html = new HtmlBuilder("a").AddAttribute(key, value);

            Assert.AreEqual("<a " + key + "=\"" + value + "\"></a>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AddAttribute_WithEmptyKeyName_ShouldNotAddAttributeToElement()
        {
            const string key = "";
            const string value = "/Home";
            var html = new HtmlBuilder("a").AddAttribute(key, value);

            Assert.AreEqual("<a></a>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AddAttributeKeyValuePair_ShouldAddAttributeToElement()
        {
            var kvp = new KeyValuePair<string, string>("href", "/Home");

            var html = new HtmlBuilder("a").AddAttribute(kvp);

            Assert.AreEqual("<a " + kvp.Key + "=\"" + kvp.Value + "\"></a>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_AppendChildren_ShouldAppendChildren()
        {
            // probably you would never do this... but it works for tests
            var html = new HtmlBuilder("div>p").AppendChildren("a");

            Assert.AreEqual("<div><p></p><a></a></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_PrependChildren_ShouldPrependChildren()
        {
            // probably you would never do this... but it works for tests
            var html = new HtmlBuilder("div>p").PrependChildren("a");

            Assert.AreEqual("<div><a></a><p></p></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_ReplaceChildren_ShouldReplaceChildren()
        {
            // probably you would never do this... but it works for tests
            var html = new HtmlBuilder("div>p").ReplaceChildren("a");

            Assert.AreEqual("<div><a></a></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_Append_ShouldReplaceElements()
        {
            // probably you would never do this... but it works for tests
            var html = new HtmlBuilder("div").Append("span");

            Assert.AreEqual("<div></div><span></span>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_Prepend_ShouldReplaceElements()
        {
            // probably you would never do this... but it works for tests
            var html = new HtmlBuilder("div").Prepend("span");

            Assert.AreEqual("<span></span><div></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_Replace_ShouldReplaceElements()
        {
            // probably you would never do this... but it works for tests
            var html = new HtmlBuilder("div").Replace("span");

            Assert.AreEqual("<span></span>", html.ToString());
        }
    }
}