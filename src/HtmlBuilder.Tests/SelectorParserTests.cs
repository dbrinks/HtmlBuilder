using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class SelectorParserTests
    {
        private SelectorParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            _parser = new SelectorParser();
        }

        [TestMethod]
        public void SelectorParser_WithTagSelector_ShouldReturnElementWithTagNameOfProvidedType()
        {
            const string tagName = "span";
            var element = _parser.Parse(tagName).First();

            Assert.AreEqual(tagName, element.TagName);
        }

        [TestMethod]
        public void SelectorParser_WithIDSelector_ShouldReturnElementWithID()
        {
            const string id = "catbug";
            var element = _parser.Parse("#" + id).First();

            Assert.AreEqual(id, element.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectorParser_WithMultiIDSelector_ShouldThrowArgumentException()
        {
            _parser.Parse("#SomeID#ThatBreaksIt");
        }

        [TestMethod]
        public void SelectorParser_WithClassSelector_ShouldReturnElementWithClass()
        {
            const string className = "catbug";
            var element = _parser.Parse("." + className).First();

            Assert.AreEqual(className, element.Classes);
        }

        [TestMethod]
        public void SelectorParser_WithMultiClassSelector_ShouldReturnElementWithMultipleClasses()
        {
            var classes = new List<string> { "gas-powered-stick", "never-runs-out-of-gas" };
            var classString = classes.Aggregate("", (cur, cls) => cur + "." + cls);
            var expectedString = classes.Aggregate("", (cur, cls) => cur + " " + cls).Trim();

            var element = _parser.Parse(classString).First();

            Assert.AreEqual(expectedString, element.Classes);
        }

        [TestMethod]
        public void SelectorParser_WithAttributeSelector_ShouldReturnElementWithAttribute()
        {
            var kvp = new KeyValuePair<string, string>("disabled", "disabled");
            var elements = _parser.Parse("[" + kvp.Key + "=\"" + kvp.Value + "\"]").First();

            var elementAttr = elements.Attributes;

            Assert.IsTrue(elementAttr.ContainsKey(kvp.Key));
            Assert.AreEqual(kvp.Value, elementAttr[kvp.Key]);
        }

        [TestMethod]
        public void SelectorParser_WithChildSelector_ShouldReturnAnElementWithAChildElement()
        {
            const string elName = "div";
            const string childName = "span";

            var element = _parser.Parse(elName + ">" + childName).First();

            Assert.AreEqual(elName, element.TagName);
            Assert.AreEqual(childName, element.Children.First().TagName);
        }

        [TestMethod]
        public void SelectorParser_WithSiblingSelector_ShouldReturnTwoTopLevelElements()
        {
            const string el = "div";
            const string sibling = "span";

            var elements = _parser.Parse(el + "+" + sibling);

            Assert.AreEqual(2, elements.Count);
            Assert.AreEqual(el, elements[0].TagName);
            Assert.AreEqual(sibling, elements[1].TagName);
        }

        [TestMethod]
        public void SelectorParser_WithCountSelector_ShouldReturnMultipleElements()
        {
            var elements = _parser.Parse("div*3");
            Assert.AreEqual(3, elements.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectorParser_WithMultiCountSelectors_ShouldThrowArgumentException()
        {
            _parser.Parse("a*3*4");
        }

    }
}