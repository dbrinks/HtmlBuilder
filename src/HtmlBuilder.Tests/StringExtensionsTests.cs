using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlBuilder.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        private const string _testStringPrefix = "Test";
        private const string _testStringSuffix = "String";
        private const char _hyphenChar = '-';
        private const string _hyphenString = "-";
        private const char _exclamationPoint = '!';
        private const char _space = ' ';
        private readonly char[] _characters = new[] { _space, _exclamationPoint };
        private readonly string _testString = _testStringPrefix + _hyphenString + _space + _testStringSuffix + _exclamationPoint;

        [TestMethod]
        public void SubstringUntil_HyphenCharacter_ReturnsPrefix()
        {
            var str = _testString.SubstringUntil(0, _hyphenChar);
            Assert.AreEqual<string>(_testStringPrefix, str);
        }

        [TestMethod]
        public void SubstringUntil_HyphenString_ReturnsPrefix()
        {
            var str = _testString.SubstringUntil(0, _hyphenString);
            Assert.AreEqual<string>(_testStringPrefix, str);
        }

        [TestMethod]
        public void SubstringUntil_ExclamationPoint_ReturnsPrefixHyphenSuffix()
        {
            var str = _testString.SubstringUntil(0, _exclamationPoint);
            Assert.AreEqual<string>(_testStringPrefix + _hyphenString + _space + _testStringSuffix, str);
        }

        [TestMethod]
        public void SubstringUntil_CharacterArray_ReturnsPrefixHyphen()
        {
            var str = _testString.SubstringUntil(0, _characters);
            Assert.AreEqual(_testStringPrefix + _hyphenChar, str);
        }
    }
}
