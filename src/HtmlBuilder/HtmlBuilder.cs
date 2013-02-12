using System;
using System.Collections.Generic;
using System.Linq;

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

            if (Selector.Contains('.'))
            {
                var index = Selector.IndexOf('.') + 1;
                element.ClassName = Selector.SubstringUntil(index, _specialCharacters);
            }

            return element.ToString();
        }
    }
}
