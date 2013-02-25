using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{
    public class HtmlBuilder
    {
        public string Selector { get; set; }
        private readonly char[] _specialCharacters = new[] { '#', '.', '[', ']', '\'', '\"', '=', '>', '{', '}' };

        /// <summary>
        /// 
        /// </summary>
        public HtmlBuilder() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        public HtmlBuilder(string selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Selector))
            {
                throw new ArgumentException("A selector is required.");
            }

            var element = new Element { TagName = Selector.SubstringUntil(0, _specialCharacters) };

            //REGEX_ID_SELECTORS = /(#[a-z]+[_a-z0-9-:\\]*)/ig;
            //REGEX_CLASS_SELECTORS = /(\.[_a-z]+[_a-z0-9-:\\]*)/ig;
            //REGEX_ATTR_SELECTORS = /(\[\s*[_a-z0-9-:\.\|\\]+\s*(?:[~\|\*\^\$]?=\s*[\"\'][^\"\']*[\"\'])?\s*\])/ig;

            var classRegex = new Regex(@"(\.[_a-z]+[_a-z0-9-:\\]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var classes = classRegex.Matches(Selector);

            if (classes.Count > 0)
            {
                var classString =
                    classes.Cast<object>().Aggregate(string.Empty,
                        (current, c) =>
                            current + (c.ToString().Replace(".", "") + " ")
                    );
                element.ClassName = classString.Trim();
            }

            return element.ToString();
        }
    }
}
