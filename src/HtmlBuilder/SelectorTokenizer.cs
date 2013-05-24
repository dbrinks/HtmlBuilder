using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public class SelectorTokenizer : ISelectorTokenizer
    {
        private static readonly char[] _specialCharacters = new[] { '#', '.', '[', ']', '>', '{', '}', '*', '+' };
        private static readonly Regex _idRegex = new Regex(@"#([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        //private static readonly Regex _textRegex = new Regex(@"\{(.+)\}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _classRegex = new Regex(@"\.([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _countRegex = new Regex(@"\*\s*[0-9]+", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _attributeRegex = new Regex(@"\[([^\]~\$\*\^\|\!]+)(=[^\]]+)?\]", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public SelectorTokens Parse(string selector)
        {
            var thisTagSelector = ParseTagSelector(selector);
            return new SelectorTokens
                {
                    Tag = ParseTag(thisTagSelector),
                    Attributes = ParseAttributes(thisTagSelector),
                    Count = ParseCount(thisTagSelector),
                    ChildSelector = ParseChildSelector(selector),
                    NextSiblingSelector = ParseNextSiblingSelector(selector)
                };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static string ParseTagSelector(string selector)
        {
            var childSymbolIndex = selector.IndexOf('>');
            var siblingSymbolIndex = selector.IndexOf('+');

            if (childSymbolIndex > -1 && siblingSymbolIndex > -1)
            {
                selector = childSymbolIndex < siblingSymbolIndex
                        ? selector.Remove(childSymbolIndex)
                        : selector.Remove(siblingSymbolIndex);
            }
            else if (childSymbolIndex > -1)
            {
                selector = selector.Remove(childSymbolIndex);
            }
            else if (siblingSymbolIndex > -1)
            {
                selector = selector.Remove(siblingSymbolIndex);

            }

            return selector.Trim();
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
        private static string ParseChildSelector(string selector)
        {
            var childSymbolIndex = selector.IndexOf('>');
            var result = string.Empty;

            if (childSymbolIndex > -1)
            {
                result = selector.Substring(childSymbolIndex + 1);
            }

            return result;
        }

        private static string ParseNextSiblingSelector(string selector)
        {
            var childSymbolIndex = selector.IndexOf('>');
            var nextSiblingSymbolIndex = selector.IndexOf('+');
            var result = string.Empty;

            // is there even a child selector?
            if (nextSiblingSymbolIndex > -1)
            {
                if (childSymbolIndex < 0 || nextSiblingSymbolIndex < childSymbolIndex)
                {
                    result = selector.Substring(nextSiblingSymbolIndex + 1);
                }
            }

            return result;
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
