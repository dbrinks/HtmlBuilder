using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{
    public static class SelectorParser
    {
        private static readonly char[] _specialCharacters = new[] { '#', '.', '[', ']', '\'', '\"', '=', '>', '{', '}' };
        private static readonly Regex _idRegex = new Regex(@"#([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex _classRegex = new Regex(@"\.([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static readonly Regex _attributeRegex = new Regex(@"\[([^\]~\$\*\^\|\!]+)(=[^\]]+)?\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Element Parse(string selector)
        {
            //var selectorChunks = selector.Split(new[] { ' ', '>' });

            var element = new Element
                {
                    TagName = ParseTag(selector),
                    Id = ParseId(selector),
                    Classes = ParseClasses(selector),
                    Attributes = ParseAttributes(selector)
                };

            return element;
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
                throw new ArgumentOutOfRangeException("selector", @"The selector provided has more than one ID for a single element.");
            }

            return idMatches.Count != 0 ? idMatches[0].Value.TrimStart('#') : string.Empty; // possibly cause weird behavior?
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static List<string> ParseClasses(string selector)
        {
            var classMatches = _classRegex.Matches(selector);
            return classMatches.Cast<Match>().Select(match => match.Value.TrimStart('.')).ToList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string ReplaceQuotes(this String str)
        {
            return str.Replace("\'", "").Replace("\"", "");
        }
    }
}
