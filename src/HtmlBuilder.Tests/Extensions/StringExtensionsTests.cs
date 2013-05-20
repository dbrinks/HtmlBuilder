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
        public void SubstringUntil_WithHyphenCharacter_ShouldReturnPrefix()
        {
            var str = _testString.SubstringUntil(0, _hyphenChar);
            Assert.AreEqual(_testStringPrefix, str);
        }

        [TestMethod]
        public void SubstringUntil_WithHyphenString_ShouldReturnPrefix()
        {
            var str = _testString.SubstringUntil(0, _hyphenString);
            Assert.AreEqual(_testStringPrefix, str);
        }

        [TestMethod]
        public void SubstringUntil_WithExclamationPoint_ShouldReturnPrefixHyphenSuffix()
        {
            var str = _testString.SubstringUntil(0, _exclamationPoint);
            Assert.AreEqual(_testStringPrefix + _hyphenString + _space + _testStringSuffix, str);
        }

        [TestMethod]
        public void SubstringUntil_WithCharacterArray_ShouldReturnPrefixHyphen()
        {
            var str = _testString.SubstringUntil(0, _characters);
            Assert.AreEqual(_testStringPrefix + _hyphenChar, str);
        }
    }
}
