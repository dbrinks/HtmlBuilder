using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{
    public class HtmlBuilder
    {
        public string Selector { get; set; }
        private readonly Element _element;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        public HtmlBuilder(string selector)
        {
            Selector = selector;

            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException();
            }

            _element = SelectorParser.Parse(Selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _element.ToString();
        }
    }
}
