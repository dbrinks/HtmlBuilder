using System;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests.Extensions
{
    [TestClass]
    public class NodeExtensionTests
    {
        private const string _tagName = "div";

        [TestMethod]
        public void AsTagNode_WithTagNode_ShouldBeCastToTagNode()
        {
            Node node = new TagNode(_tagName);
            var castNode = node.AsTagNode();

            Assert.IsInstanceOfType(castNode, typeof(TagNode));
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void AsTagNode_WithNonTagNode_ShouldThrowInvalidCastException()
        {
            Node node = new TextNode(string.Empty);
            node.AsTagNode();
        }

        [TestMethod]
        public void AsTextNode_WithTextNode_ShouldBeCastToTextNode()
        {
            Node node = new TextNode(string.Empty);
            var castNode = node.AsTextNode();

            Assert.IsInstanceOfType(castNode, typeof(TextNode));
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void AsTextNode_WithNonTextNode_ShouldThrowInvalidCastException()
        {
            Node node = new TagNode(_tagName);
            node.AsTextNode();
        }
    }
}
