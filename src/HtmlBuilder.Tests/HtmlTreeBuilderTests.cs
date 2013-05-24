using System.Linq;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class HtmlTreeBuilderTests
    {
        private HtmlTreeBuilder _builder;

        [TestMethod]
        public void Build_WithChildSelector_ShouldReturnAnElementWithAChildElement()
        {
            const string elName = "div";
            const string childName = "span";
            const string selector = elName + ">" + childName;

            var mock = new Mock<ISelectorTokenizer>();
            mock.Setup(tokenizer => tokenizer.Parse(selector))
                .Returns(new SelectorTokens
                {
                    Tag = elName,
                    ChildSelector = childName,
                    Count = 1
                });
            mock.Setup(tokenizer => tokenizer.Parse(childName))
                .Returns(new SelectorTokens
                {
                    Tag = childName,
                    Count = 1
                });

            SetUpBuilder(mock.Object);

            var tree = _builder.Build(elName + ">" + childName);

            mock.Verify(tokenizer => tokenizer.Parse(It.IsAny<string>()), Times.Exactly(2));
            Assert.IsInstanceOfType(tree.Current, typeof(TagNode));
            Assert.AreEqual(elName, tree.Current.AsTagNode().TagName);
            Assert.IsInstanceOfType(tree.Current.Children.First(), typeof(TagNode));
            Assert.AreEqual(childName, tree.Current.Children.First().AsTagNode().TagName);
        }

        [TestMethod]
        public void Build_WithSiblingSelector_ShouldReturnTwoTopLevelElements()
        {
            const string el = "div";
            const string sibling = "span";
            const string selector = el + "+" + sibling;

            var mock = new Mock<ISelectorTokenizer>();
            mock.Setup(tokenizer => tokenizer.Parse(selector))
                .Returns(() => new SelectorTokens
                    {
                        Tag = el,
                        NextSiblingSelector = sibling,
                        Count = 1
                    });
            mock.Setup(tokenizer => tokenizer.Parse(sibling))
                .Returns(() => new SelectorTokens
                    {
                        Tag = sibling,
                        Count = 1
                    });

            SetUpBuilder(mock.Object);

            var tree = _builder.Build(selector);

            mock.Verify(tokenizer => tokenizer.Parse(It.IsAny<string>()), Times.Exactly(2));
            Assert.IsInstanceOfType(tree.Current, typeof(TagNode));
            Assert.AreEqual(el, tree.Current.AsTagNode().TagName);
            Assert.IsInstanceOfType(tree.Current.NextSibling, typeof(TagNode));
            Assert.AreEqual(sibling, tree.Current.NextSibling.AsTagNode().TagName);
        }

        [TestMethod]
        public void Build_WithCountSelector_ShouldReturnMultipleElements()
        {
            const string elName = "div";
            const string selector = elName + "*3";

            var mock = new Mock<ISelectorTokenizer>();
            mock.Setup(tokenizer => tokenizer.Parse(selector))
                .Returns(() => new SelectorTokens
                    {
                        Tag = elName,
                        Count = 3
                    });

            SetUpBuilder(mock.Object);

            var tree = _builder.Build(selector);

            mock.Verify(tokenizer => tokenizer.Parse(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(elName, tree.Current.AsTagNode().TagName);
            Assert.AreEqual(elName, tree.Current.NextSibling.AsTagNode().TagName);
            Assert.AreEqual(elName, tree.Current.NextSibling.NextSibling.AsTagNode().TagName);
        }

        [TestMethod]
        public void Build_WithMultiplierAndSibling_ShouldReturnXNodesPlusOneOtherNode()
        {
            const string elName = "div";
            const string sibling = "span";
            const string selector = elName + "*2+" + sibling;

            var mock = new Mock<ISelectorTokenizer>();
            mock.Setup(tokenizer => tokenizer.Parse(selector))
                .Returns(() => new SelectorTokens
                    {
                        Tag = elName,
                        NextSiblingSelector = sibling,
                        Count = 2
                    });
            mock.Setup(tokenizer => tokenizer.Parse(sibling))
                .Returns(() => new SelectorTokens
                {
                    Tag = sibling,
                    Count = 2
                });

            SetUpBuilder(mock.Object);

            var tree = _builder.Build(selector);

            Assert.AreEqual(elName, tree.Current.AsTagNode().TagName);
            Assert.AreEqual(elName, tree.Current.NextSibling.AsTagNode().TagName);
            Assert.AreEqual(sibling, tree.Current.NextSibling.NextSibling.AsTagNode().TagName);
        }

        public void SetUpBuilder(ISelectorTokenizer tokenizer)
        {
            _builder = new HtmlTreeBuilder(tokenizer);
        }
    }
}