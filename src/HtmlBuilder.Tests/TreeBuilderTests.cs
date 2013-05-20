using System;
using System.Collections.Generic;
using System.Linq;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class TreeBuilderTests
    {
        private HtmlTreeBuilder _parser;

        [TestInitialize]
        public void Initialize()
        {
            _parser = new HtmlTreeBuilder();
        }

        [TestMethod]
        public void Parse_WithTagSelector_ShouldReturnElementWithTagNameOfProvidedType()
        {
            const string tagName = "span";
            var element = _parser.Build(tagName).First();

            Assert.AreEqual(tagName, element.AsTagNode().TagName);
        }

        [TestMethod]
        public void Parse_WithIDSelector_ShouldReturnElementWithID()
        {
            const string id = "catbug";
            var element = _parser.Build("#" + id).First();

            Assert.AreEqual(id, element.AsTagNode().Id);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Parse_WithMultiIDSelector_ShouldThrowArgumentException()
        {
            _parser.Build("#SomeID#ThatBreaksIt");
        }

        [TestMethod]
        public void Parse_WithClassSelector_ShouldReturnElementWithClass()
        {
            const string className = "catbug";
            var element = _parser.Build("." + className).First();

            Assert.IsInstanceOfType(element, typeof(TagNode));
            Assert.AreEqual(className, ((TagNode)element).Classes);
        }

        [TestMethod]
        public void Parse_WithMultiClassSelector_ShouldReturnElementWithMultipleClasses()
        {
            var classes = new List<string> { "gas-powered-stick", "never-runs-out-of-gas" };
            var classString = classes.Aggregate("", (cur, cls) => cur + "." + cls);
            var expectedString = classes.Aggregate("", (cur, cls) => cur + " " + cls).Trim();

            var element = _parser.Build(classString).First();

            Assert.IsInstanceOfType(element, typeof(TagNode));
            Assert.AreEqual(expectedString, element.AsTagNode().Classes);
        }

        [TestMethod]
        public void Parse_WithAttributeSelector_ShouldReturnElementWithAttribute()
        {
            var kvp = new KeyValuePair<string, string>("disabled", "disabled");
            var element = _parser.Build("[" + kvp.Key + "=\"" + kvp.Value + "\"]").First();

            Assert.IsInstanceOfType(element, typeof(TagNode));
            Assert.AreEqual(kvp.Value, element.AsTagNode().GetAttribute(kvp.Key));
        }

        [TestMethod]
        public void Parse_WithChildSelector_ShouldReturnAnElementWithAChildElement()
        {
            const string elName = "div";
            const string childName = "span";

            var element = _parser.Build(elName + ">" + childName).First();

            Assert.IsInstanceOfType(element, typeof(TagNode));
            Assert.AreEqual(elName, element.AsTagNode().TagName);
            Assert.IsInstanceOfType(element.Children.First(), typeof(TagNode));
            Assert.AreEqual(childName, element.Children.First().AsTagNode().TagName);
        }

        [TestMethod]
        public void Parse_WithSiblingSelector_ShouldReturnTwoTopLevelElements()
        {
            const string el = "div";
            const string sibling = "span";

            var elements = _parser.Build(el + "+" + sibling);

            Assert.AreEqual(2, elements.Count);
            Assert.IsInstanceOfType(elements[0], typeof(TagNode));
            Assert.AreEqual(el, elements[0].AsTagNode().TagName);
            Assert.IsInstanceOfType(elements[1], typeof(TagNode));
            Assert.AreEqual(sibling, elements[1].AsTagNode().TagName);
        }

        [TestMethod]
        public void Parse_WithCountSelector_ShouldReturnMultipleElements()
        {
            var elements = _parser.Build("div*3");
            Assert.AreEqual(3, elements.Count());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Parse_WithMultiCountSelectors_ShouldThrowArgumentException()
        {
            _parser.Build("a*3*4");
        }

    }
}