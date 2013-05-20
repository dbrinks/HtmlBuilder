using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public class HtmlTreeBuilder
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
        public List<Node> Build(string selector)
        {
            var elements = new List<Node>();
            List<Node> children = null;
            List<Node> siblings = null;

            selector = selector.Trim();

            var childSelectorIndex = selector.IndexOf('>');

            if (childSelectorIndex >= 0)
            {
                children = Build(selector.Substring(childSelectorIndex + 1));
                selector = selector.Substring(0, childSelectorIndex).Trim();
            }

            var siblingSelectorIndex = selector.IndexOf('+');

            if (siblingSelectorIndex >= 0)
            {
                siblings = Build(selector.Substring(siblingSelectorIndex + 1));
                selector = selector.Substring(0, siblingSelectorIndex).Trim();
            }

            var count = ParseCount(selector);

            for (var i = 0; i < count; i++)
            {
                var element = CreateElement(selector);

                if (children != null)
                {
                    element.Children = children;
                }

                elements.Add(element);
            }

            if (siblings != null)
            {
                elements.AddRange(siblings);
            }

            return elements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static TagNode CreateElement(string selector)
        {
            var attr = ParseAttributes(selector);
            var id = ParseId(selector);
            var classes = ParseClasses(selector);

            if (!string.IsNullOrEmpty(id))
                attr.Add("id", id);

            if (!string.IsNullOrEmpty(classes))
                attr.Add("class", classes);

            var node = new TagNode(ParseTag(selector));
            node.SetAttributes(attr);

            return node;
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
                throw new ArgumentException();
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
            var idMatches = _idRegex.Matches(selector);

            if (idMatches.Count > 1)
            {
                throw new ArgumentException(@"The selector provided has more than one ID for a single element.", "selector");
            }

            return idMatches.Count != 0 ? idMatches[0].Value.TrimStart('#') : string.Empty; // possibly cause weird behavior?
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static string ParseClasses(string selector)
        {
            var classMatches = _classRegex.Matches(selector);

            return classMatches.Cast<Match>().Aggregate("", (current, match) => current + " " + match.Value.TrimStart('.')).Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ParseAttributes(string selector)
        {
            var attrMatches = _attributeRegex.Matches(selector);

            var attributePairs = attrMatches.Cast<Match>().Select(m => m.Value.Trim(new[] { '[', ']' })).ToList();

            return attributePairs.Select(pair => pair.Split('=')).ToDictionary(s => s[0], s => s.Count() > 1 ? s[1].ReplaceQuotes() : string.Empty);
        }
    }
}
