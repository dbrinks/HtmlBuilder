using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests.Models
{
    [TestClass]
    public class TextNodeTests
    {
        [TestMethod]
        public void Constructor_WithText_ShouldSetTextProperty()
        {
            const string text = "test";
            var node = new TextNode(text);

            Assert.AreEqual(text, node.Text);
        }

        [TestMethod]
        public void ToString_WithTextNotNull_ShouldReturnText()
        {
            const string text = "test";
            var node = new TextNode(text);

            Assert.AreEqual(text, node.ToString());
        }

        [TestMethod]
        public void ToString_WithTextNull_ShouldReturnEmptyString()
        {
            var node = new TextNode(null);

            Assert.AreEqual(string.Empty, node.ToString());
        }
    }
}
