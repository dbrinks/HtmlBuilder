using System;
using System.Collections.Generic;
using HtmlBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests.Models
{
    [TestClass]
    public class TagNodeTests
    {
        private const string _tagName = "div";
        private const string _classKey = "class";
        private const string _classValue = "test";
        private const string _idKey = "id";

        [TestMethod]
        public void Constructor_WithTagName_ShouldSetTagName()
        {
            var node = new TagNode(_tagName);

            Assert.AreEqual(_tagName, node.TagName);
        }

        [TestMethod]
        public void GetAttribute_WithExistingKey_ShouldReturnCorrectValue()
        {
            var node = GetTagNodeWithAttributes();

            Assert.AreEqual(_classValue, node.GetAttribute(_classKey));
        }

        [TestMethod]
        public void GetAttribute_WithNonExistantKey_ShouldReturnEmptyString()
        {
            var node = new TagNode(_tagName);

            Assert.AreEqual(string.Empty, node.GetAttribute(_classKey));
        }

        [TestMethod]
        public void SetAttributes_WithAttributeDictionary_ShouldOverwriteExistingAttributes()
        {
            const string newValue = "second";
            var newAttributes = GetAttributes(_classKey, newValue);

            var node = GetTagNodeWithAttributes();
            node.SetAttributes(newAttributes);

            Assert.AreEqual(newValue, node.GetAttribute(_classKey));
        }

        [TestMethod]
        public void AddAttribute_WithValidNewKeyAndValue_ShouldAddKeyValueToAttributes()
        {
            var node = new TagNode(_tagName);
            node.AddAttribute(_classKey, _classValue);

            Assert.AreEqual(_classValue, node.GetAttribute(_classKey));
        }

        [TestMethod]
        public void AddAttribute_WithValidKeyAndValue_ShouldAddValueToCurrenetAttribute()
        {
            const string value2 = "second";
            var node = new TagNode(_tagName);
            node.AddAttribute(_classKey, _classValue);
            node.AddAttribute(_classKey, value2);

            Assert.AreEqual(_classValue + " " + value2, node.GetAttribute(_classKey));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddAttribute_WithNullKey_ShouldThrowArgumentException()
        {
            var node = new TagNode(_tagName);
            node.AddAttribute(null, _classValue);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddAttribute_WithEmptyStringKey_ShouldThrowArgumentException()
        {
            var node = new TagNode(_tagName);
            node.AddAttribute(string.Empty, _classValue);
        }

        [TestMethod]
        public void SetAttribute_WithValidNewKeyAndValue_ShouldAddKeyValueToAttributes()
        {
            var node = new TagNode(_tagName);
            node.SetAttribute(_classKey, _classValue);

            Assert.AreEqual(_classValue, node.GetAttribute(_classKey));
        }

        [TestMethod]
        public void SetAttribute_WithValidKeyAndValue_ShouldAddValueToCurrenetAttribute()
        {
            const string value2 = "second";
            var node = new TagNode(_tagName);
            node.SetAttribute(_classKey, _classValue);
            node.SetAttribute(_classKey, value2);

            Assert.AreEqual(value2, node.GetAttribute(_classKey));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetAttribute_WithNullKey_ShouldThrowArgumentException()
        {
            var node = new TagNode(_tagName);
            node.SetAttribute(null, _classValue);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetAttribute_WithEmptyStringKey_ShouldThrowArgumentException()
        {
            var node = new TagNode(_tagName);
            node.SetAttribute(string.Empty, _classValue);
        }

        [TestMethod]
        public void Classes_ShouldReturnClassAttribute()
        {
            var node = GetTagNodeWithAttributes();

            Assert.AreEqual(_classValue, node.Classes);
        }

        [TestMethod]
        public void Classes_ShouldSetClassAttribute()
        {
            var node = new TagNode(_tagName);
            node.Classes = _classValue;

            Assert.AreEqual(_classValue, node.Classes);
        }

        [TestMethod]
        public void Id_ShouldReturnIdAttribute()
        {
            const string value = "unique";
            var node = new TagNode(_tagName);
            node.AddAttribute(_idKey, value);

            Assert.AreEqual(value, node.Id);
        }

        [TestMethod]
        public void Id_ShouldSetIdAttribute()
        {
            const string value = "unique";
            var node = new TagNode(_tagName);
            node.Id = value;

            Assert.AreEqual(value, node.Id);
        }

        [TestMethod]
        public void GetData_ShouldReturnDataAttribute()
        {
            const string key = "keys";
            const string value = "[1,5,23]";

            var node = new TagNode(_tagName);
            node.AddAttribute("data-" + key, value);

            Assert.AreEqual(value, node.GetData(key));
        }

        [TestMethod]
        public void AddData_ShouldAddDataAttribute()
        {
            const string key = "keys";
            const string value = "[1,5,23]";

            var node = new TagNode(_tagName);
            node.AddData(key, value);

            Assert.AreEqual(value, node.GetData(key));
        }

        #region Static Helpers
        private static Dictionary<string, string> GetAttributes(string key, string value)
        {
            return new Dictionary<string, string>
                {
                    {key, value}
                };
        }

        private static TagNode GetTagNodeWithAttributes()
        {
            var attributes = GetAttributes(_classKey, _classValue);
            var node = new TagNode(_tagName);
            node.SetAttributes(attributes);

            return node;
        }
        #endregion
    }
}
