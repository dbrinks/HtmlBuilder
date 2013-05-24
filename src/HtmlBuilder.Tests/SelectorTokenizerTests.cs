using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class SelectorTokenizerTests
    {
        /// <summary>
        /// 
        /// </summary>
        private SelectorTokenizer _tokenizer;

        [TestInitialize]
        public void SetUp()
        {
            _tokenizer = new SelectorTokenizer();
        }

        [TestMethod]
        public void Parse_WithTagSelector_ShouldReturnElementWithTagNameOfProvidedType()
        {
            const string tagName = "span";
            var tokens = _tokenizer.Parse(tagName);

            Assert.AreEqual(tagName, tokens.Tag);
        }

        [TestMethod]
        public void Parse_WithIDSelector_ShouldReturnElementWithID()
        {
            const string id = "catbug";
            var tokens = _tokenizer.Parse("#" + id);

            Assert.AreEqual(id, tokens.Attributes["id"]);
        }

        [TestMethod]
        public void Parse_WithClassSelector_ShouldReturnElementWithClass()
        {
            const string className = "catbug";
            var token = _tokenizer.Parse("." + className);

            Assert.AreEqual(className, token.Attributes["class"]);
        }

        [TestMethod]
        public void Parse_WithMultiClassSelector_ShouldReturnElementWithMultipleClasses()
        {
            var classes = new List<string> { "gas-powered-stick", "never-runs-out-of-gas" };
            var classString = classes.Aggregate("", (cur, cls) => cur + "." + cls);
            var expectedString = classes.Aggregate("", (cur, cls) => cur + " " + cls).Trim();

            var token = _tokenizer.Parse(classString);

            Assert.AreEqual(expectedString, token.Attributes["class"]);
        }

        [TestMethod]
        public void Parse_WithAttributeSelector_ShouldReturnElementWithAttribute()
        {
            var kvp = new KeyValuePair<string, string>("disabled", "disabled");
            var tokens = _tokenizer.Parse("[" + kvp.Key + "=\"" + kvp.Value + "\"]");

            Assert.AreEqual(kvp.Value, tokens.Attributes[kvp.Key]);
        }

        [TestMethod]
        public void Parse_WithCountSelector_ShouldReturnMultipleElements()
        {
            var elements = _tokenizer.Parse("div*3");
            Assert.AreEqual(3, elements.Count);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Parse_WithMultiCountSelectors_ShouldThrowArgumentException()
        {
            _tokenizer.Parse("a*3*4");
        }

        [TestMethod]
        public void Parse_WithChildSelector_ShouldReturnSelectorOfChildNode()
        {
            const string parent = "div";
            const string childSelector = "span*2>a";
            var token = _tokenizer.Parse(parent + ">" + childSelector);

            Assert.AreEqual(childSelector, token.ChildSelector);
        }

        [TestMethod]
        public void Parse_WithSiblingSelector_ShouldReturnSelectorOfNextSibling()
        {
            const string root = "div";
            const string siblingSelector = "a";
            var token = _tokenizer.Parse(root + "+" + siblingSelector);

            Assert.AreEqual(siblingSelector, token.NextSiblingSelector);
        }

        [TestMethod]
        public void Parse_WithSiblingThenChildSelector_ShouldReturnSelectorOfNextSibling()
        {
            const string root = "div";
            const string siblingSelector = "a>span";
            var token = _tokenizer.Parse(root + "+" + siblingSelector);

            Assert.AreEqual(siblingSelector, token.NextSiblingSelector);
        }

        [TestMethod]
        public void Parse_WithChildThenSiblingSelector_ShouldReturnSelectorOfNextSibling()
        {
            const string root = "div";
            const string siblingSelector = "a";
            const string childSelector = "span";
            var token = _tokenizer.Parse(root + ">" + childSelector + "+" + siblingSelector);

            Assert.AreEqual(string.Empty, token.NextSiblingSelector);
        }

        [TestMethod]
        public void Parse_WithSelectorContainingChildThatHasAnID_ShouldAssignChildTheID()
        {
            const string root = "div";
            const string childSelector = "span";
            const string id = "test";
            var token = _tokenizer.Parse(root + ">" + childSelector + "#" + id);

            Assert.AreEqual(root, token.Tag);
            Assert.AreEqual(0, token.Attributes.Count);
        }

        [TestMethod]
        public void Parse_WithSelectorContainingSiblingThatHasAnID_ShouldAssignSiblingTheID()
        {
            const string root = "div";
            const string sibling = "span";
            const string id = "test";
            var token = _tokenizer.Parse(root + "+" + sibling + "#" + id);

            Assert.AreEqual(root, token.Tag);
            Assert.AreEqual(0, token.Attributes.Count);
        }
    }
}
