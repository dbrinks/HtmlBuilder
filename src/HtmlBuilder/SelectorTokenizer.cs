using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public static class SelectorTokenizer
    {
        private static readonly char[] _specialCharacters = new[] { '#', '.', '[', ']', '>', '{', '}', '*', '+' };
        private static readonly Regex _idRegex = new Regex(@"#([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _textRegex = new Regex(@"\{(.+)\}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _classRegex = new Regex(@"\.([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _countRegex = new Regex(@"\*\s*[0-9]+", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _attributeRegex = new Regex(@"\[([^\]~\$\*\^\|\!]+)(=[^\]]+)?\]", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static SelectorTokens Parse(string selector)
        {
            return new SelectorTokens
                {
                    Tag = ParseTag(selector),
                    Attributes = ParseAttributes(selector),
                    Count = ParseCount(selector)
                };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static int ParseCount(string selector)
        {
            var countMatches = _countRegex.Matches(selector);

            if (countMatches.Count > 1)
            {
                throw new ArgumentException("Selectors can only contain one multiplier (*) per group");
            }

            var count = 1;

            if (countMatches.Count > 0)
            {
                count = int.Parse(countMatches[0].Value.TrimStart('*'));
            }

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static string ParseTag(string selector)
        {
            return selector.SubstringUntil(0, _specialCharacters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static string ParseId(string selector)
        {
            return _idRegex.Matches(selector).ToIDString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static string ParseClasses(string selector)
        {
            return _classRegex.Matches(selector).ToCSSClassString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ParseAttributes(string selector)
        {
            var attributeDict = _attributeRegex.Matches(selector).ToAttributeDictionary();
            var id = ParseId(selector);
            var classes = ParseClasses(selector);

            if (!string.IsNullOrEmpty(id))
            {
                attributeDict.Add("id", id);
            }

            if (!string.IsNullOrEmpty(classes))
            {
                attributeDict.Add("class", classes);
            }

            return attributeDict;
        }
    }
}
