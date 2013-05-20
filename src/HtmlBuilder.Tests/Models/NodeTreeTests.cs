using System;
using System.Collections.Generic;
using System.Linq;
using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class NodeTreeTests
    {
        private const string _tagName = "div";

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constuctor_WithNullNode_ShouldThrowArgumentNullException()
        {
            Node node = null;
            var tree = new NodeTree(node);
        }

        [TestMethod]
        public void Constuctor_WithNode_Should()
        {
            var node = GetTagNode();
            var tree = new NodeTree(node);

            Assert.AreEqual(node, tree.Root);
            Assert.AreEqual(node, tree.Current);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Constuctor_WithNullNodeList_ShouldThrowArgumentNullException()
        //{
        //    List<Node> nodes = null;
        //    var tree = new NodeTree(nodes);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Constuctor_WithEmptyNodeList_ShouldThrowArgumentException()
        //{
        //    var nodes = new List<Node>();
        //    var tree = new NodeTree(nodes);
        //}

        //[TestMethod]
        //public void Constuctor_WithNodeList_Should()
        //{
        //    var nodes = GetNodeList();
        //    var tree = new NodeTree(nodes);

        //    CollectionAssert.AreEqual(nodes, tree.Nodes);
        //    Assert.AreEqual(nodes.First(), tree.Root);
        //    Assert.AreEqual(nodes.First(), tree.Current);
        //}

        [TestMethod]
        public void Next_WithNodeThatHasNextSibling_ShouldReturnTrueAndMoveCurrentToCurrentsNextSibling()
        {
            var next = GetTagNode();
            var node = GetTagNodeWithNextSibling(next);
            var tree = new NodeTree(node);
            var result = tree.Next();

            Assert.IsTrue(result);
            Assert.AreEqual(next, tree.Current);
        }

        [TestMethod]
        public void Next_WithNodeWithoutNextSibling_ShouldReturnFalseAndNotMoveCurrent()
        {
            var node = GetTagNode();
            var tree = new NodeTree(node);
            var result = tree.Next();

            Assert.IsFalse(result);
            Assert.AreEqual(node, tree.Current);
        }

        [TestMethod]
        public void Previous_WithNodeThatHasPreviousSibling_ShouldReturnTrueAndMoveCurrentToCurrentsPreviousSibling()
        {
            var prev = GetTagNode();
            var node = GetTagNodeWithPrevSibling(prev);
            var tree = new NodeTree(node);
            var result = tree.Previous();

            Assert.IsTrue(result);
            Assert.AreEqual(prev, tree.Current);
        }

        [TestMethod]
        public void Previous_WithNodeWithoutPreviousSibling_ShouldReturnFalseAndNotMoveCurrent()
        {
            var node = GetTagNode();
            var tree = new NodeTree(node);
            var result = tree.Previous();

            Assert.IsFalse(result);
            Assert.AreEqual(node, tree.Current);
        }

        [TestMethod]
        public void Parent_WithNodeWithParent_ShouldReturnTrueAndMoveCurrentToCurrentsParent()
        {
            var parent = GetTagNode();
            var node = GetTagNodeWithParent(parent);
            var tree = new NodeTree(node);
            var result = tree.Parent();

            Assert.IsTrue(result);
            Assert.AreEqual(parent, tree.Current);
        }

        [TestMethod]
        public void Parent_WithNodeWithoutParent_ShouldReturnFalseAndNotMoveCurrent()
        {
            var node = GetTagNode();
            var tree = new NodeTree(node);
            var result = tree.Parent();

            Assert.IsFalse(result);
            Assert.AreEqual(node, tree.Current);
        }

        [TestMethod]
        public void FirstChild_WithNodeWithChildren_ShouldReturnTrueAndMoveCurrentToCurrentsFirstChild()
        {
            var first = GetTagNode();
            var last = GetTagNode();
            var node = GetTagNodeWithChildren(first, last);
            var tree = new NodeTree(node);
            var result = tree.FirstChild();

            Assert.IsTrue(result);
            Assert.AreEqual(first, tree.Current);
        }

        [TestMethod]
        public void FirstChild_WithNodeWithoutChildren_ShouldReturnFalseAndNotMoveCurrent()
        {
            var node = GetTagNode();
            var tree = new NodeTree(node);
            var result = tree.FirstChild();

            Assert.IsFalse(result);
            Assert.AreEqual(node, tree.Current);
        }

        [TestMethod]
        public void LastChild_WithNodeWithChildren_ShouldReturnTrueAndMoveCurrentToCurrentsLastChild()
        {
            var first = GetTagNode();
            var last = GetTagNode();
            var node = GetTagNodeWithChildren(first, last);
            var tree = new NodeTree(node);
            var result = tree.LastChild();

            Assert.IsTrue(result);
            Assert.AreEqual(last, tree.Current);
        }

        [TestMethod]
        public void LastChild_WithNodeWithoutChildren_ShouldReturnFalseAndNotMoveCurrent()
        {
            var node = GetTagNode();
            var tree = new NodeTree(node);
            var result = tree.LastChild();

            Assert.IsFalse(result);
            Assert.AreEqual(node, tree.Current);
        }

        #region Static Helpers
        private static Node GetTagNode()
        {
            return new TagNode(_tagName);
        }

        private static Node GetTagNodeWithNextSibling(Node sibling)
        {
            return new TagNode(_tagName)
                {
                    NextSibling = sibling
                };
        }

        private static Node GetTagNodeWithPrevSibling(Node sibling)
        {
            return new TagNode(_tagName)
                {
                    PreviousSibling = sibling
                };
        }

        private static Node GetTagNodeWithParent(Node parent)
        {
            return new TagNode(_tagName)
                {
                    Parent = parent
                };
        }

        private static Node GetTagNodeWithChildren(Node first, Node last)
        {
            return new TagNode(_tagName)
            {
                Children = new List<Node> { first, last }
            };
        }
        #endregion
    }
}
