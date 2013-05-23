using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class SelectorTokenizerTests
    {
        [TestMethod]
        public void Parse_WithTagSelector_ShouldReturnElementWithTagNameOfProvidedType()
        {
            const string tagName = "span";
            var tokens = SelectorTokenizer.Parse(tagName);

            Assert.AreEqual(tagName, tokens.Tag);
        }

        [TestMethod]
        public void Parse_WithIDSelector_ShouldReturnElementWithID()
        {
            const string id = "catbug";
            var tokens = SelectorTokenizer.Parse("#" + id);

            Assert.AreEqual(id, tokens.Attributes["id"]);
        }

        [TestMethod]
        public void Parse_WithClassSelector_ShouldReturnElementWithClass()
        {
            const string className = "catbug";
            var token = SelectorTokenizer.Parse("." + className);

            Assert.AreEqual(className, token.Attributes["class"]);
        }

        [TestMethod]
        public void Parse_WithMultiClassSelector_ShouldReturnElementWithMultipleClasses()
        {
            var classes = new List<string> { "gas-powered-stick", "never-runs-out-of-gas" };
            var classString = classes.Aggregate("", (cur, cls) => cur + "." + cls);
            var expectedString = classes.Aggregate("", (cur, cls) => cur + " " + cls).Trim();

            var token = SelectorTokenizer.Parse(classString);

            Assert.AreEqual(expectedString, token.Attributes["class"]);
        }

        [TestMethod]
        public void Parse_WithAttributeSelector_ShouldReturnElementWithAttribute()
        {
            var kvp = new KeyValuePair<string, string>("disabled", "disabled");
            var tokens = SelectorTokenizer.Parse("[" + kvp.Key + "=\"" + kvp.Value + "\"]");

            Assert.AreEqual(kvp.Value, tokens.Attributes[kvp.Key]);
        }

        [TestMethod]
        public void Parse_WithCountSelector_ShouldReturnMultipleElements()
        {
            var elements = SelectorTokenizer.Parse("div*3");
            Assert.AreEqual(3, elements.Count);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Parse_WithMultiCountSelectors_ShouldThrowArgumentException()
        {
            SelectorTokenizer.Parse("a*3*4");
        }
    }
}
