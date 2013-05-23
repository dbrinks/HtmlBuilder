using System;
using System.Collections.Generic;
using System.Linq;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class HtmlTreeBuilderTests
    {
        private HtmlTreeBuilder Builder;

        [TestInitialize]
        public void Initialize()
        {
            Builder = new HtmlTreeBuilder();
        }

        [TestMethod]
        public void Parse_WithChildSelector_ShouldReturnAnElementWithAChildElement()
        {
            const string elName = "div";
            const string childName = "span";

            var element = Builder.Build(elName + ">" + childName).First();

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

            var elements = Builder.Build(el + "+" + sibling);

            Assert.AreEqual(2, elements.Count);
            Assert.IsInstanceOfType(elements[0], typeof(TagNode));
            Assert.AreEqual(el, elements[0].AsTagNode().TagName);
            Assert.IsInstanceOfType(elements[1], typeof(TagNode));
            Assert.AreEqual(sibling, elements[1].AsTagNode().TagName);
        }

        [TestMethod]
        public void Parse_WithCountSelector_ShouldReturnMultipleElements()
        {
            var elements = Builder.Build("div*3");
            Assert.AreEqual(3, elements.Count());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Parse_WithMultiCountSelectors_ShouldThrowArgumentException()
        {
            Builder.Build("a*3*4");
        }

    }
}