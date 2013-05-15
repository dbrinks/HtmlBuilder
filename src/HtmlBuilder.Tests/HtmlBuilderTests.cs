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
            var html = new HtmlBuilder("div");

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
            const string id = "unique";
            var html = new HtmlBuilder("#" + id);

            Assert.AreEqual("<div id=\"" + id + "\"></div>", html.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HtmlBuilder_WithMultipleIds_ShouldThrowArgumentException()
        {
            var html = new HtmlBuilder("#one#two");
            html.ToString();
        }

        [TestMethod]
        public void HtmlBuilder_WithClassPassed_ShouldRenderDivWithClass()
        {
            const string cls = "item";
            var html = new HtmlBuilder("." + cls);

            Assert.AreEqual("<div class=\"" + cls + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithTagNameClass_ShouldRenderTagWithClass()
        {
            const string cls = "item";
            var html = new HtmlBuilder("span." + cls);

            Assert.AreEqual("<span class=\"" + cls + "\"></span>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithMultipleClasses_ShouldRenderDivWithMultipleClasses()
        {
            const string one = "one";
            const string two = "two";
            var html = new HtmlBuilder("." + one + "." + two);

            Assert.AreEqual("<div class=\"" + one + " " + two + "\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithAttributeWithNoValue_ShouldRenderDivWithTheAttributeName()
        {
            const string disabled = "disabled";
            var html = new HtmlBuilder("[" + disabled + "]");

            Assert.AreEqual("<div " + disabled + "=\"\"></div>", html.ToString());
        }

        [TestMethod]
        public void HtmlBuilder_WithAttributeWithValue_ShouldRenderDivWithAttribute()
        {
            const string disabled = "disabled";
            var html = new HtmlBuilder("[" + disabled + "='" + disabled + "']");

            Assert.AreEqual("<div " + disabled + "=\"" + disabled + "\"></div>", html.ToString());
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