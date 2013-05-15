using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

// Settings Ideas...
//
// Self closing tags if possible
//
//

namespace HtmlBuilder
{
    public class HtmlBuilder
    {
        public string Selector { get; set; }
        private List<HtmlElement> _elements;
        private readonly SelectorParser _parser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        public HtmlBuilder(string selector)
        {
            _parser = new SelectorParser();
            Selector = selector;

            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException();
            }

            _elements = _parser.Parse(Selector);
        }

        /// <summary>
        /// Adds a sigle CSS class to the top level elements
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public HtmlBuilder AddCSSClass(string className)
        {
            return AddCSSClasses(new[] { className });
        }

        /// <summary>
        /// Add CSS classes to the top level elements
        /// </summary>
        /// <param name="classNames"></param>
        /// <returns></returns>
        public HtmlBuilder AddCSSClasses(IEnumerable<string> classNames)
        {
            var names = classNames as string[] ?? classNames.ToArray();

            foreach (var element in _elements)
            {
                element.Classes.AddRange(names);
            }

            return this;
        }

        /// <summary>
        /// Adds an attribute to the top level elements
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlBuilder AddAttribute(string key, string value)
        {
            return AddAttributes(new Dictionary<string, string> { { key, value } });
        }

        /// <summary>
        /// Adds an attribute to the top level elements
        /// </summary>
        /// <param name="keyValuePair"></param>
        /// <returns></returns>
        public HtmlBuilder AddAttribute(KeyValuePair<string, string> keyValuePair)
        {
            return AddAttributes(new Dictionary<string, string> { { keyValuePair.Key, keyValuePair.Value } });
        }

        /// <summary>
        /// Adds a dictionary of attributes to each of the top level elements
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public HtmlBuilder AddAttributes(Dictionary<string, string> keyValuePairs)
        {
            foreach (var element in _elements)
            {
                foreach (var kvp in keyValuePairs.Where(pairs => !string.IsNullOrEmpty(pairs.Key)))
                {
                    element.Attributes.Add(kvp.Key, kvp.Value);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds an ID to the top level element.  Cannot add the ID to multiple top level
        /// elements... because it would then not be unique
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlBuilder AddID(string id)
        {
            if (_elements.Count > 1)
            {
                throw new ArgumentException("Cannot add the same ID to multiple elements.");
            }

            // Not possible to instantiate an HtmlBuilder without having 
            // at least one element be generated... assume this is safe
            _elements.First().Id = id;

            return this;
        }

        public HtmlBuilder AppendChildren(string selector)
        {
            var children = _parser.Parse(selector);

            foreach (var el in _elements)
            {
                el.Children.AddRange(children);
            }

            return this;
        }

        public HtmlBuilder PrependChildren(string selector)
        {
            var children = _parser.Parse(selector);

            foreach (var el in _elements)
            {
                el.Children.InsertRange(0, children);
            }

            return this;
        }

        public HtmlBuilder ReplaceChildren(string selector)
        {
            var children = _parser.Parse(selector);

            foreach (var el in _elements)
            {
                el.Children = children;
            }

            return this;
        }

        public HtmlBuilder Append(string selector)
        {
            var elements = _parser.Parse(selector);

            _elements.AddRange(elements);

            return this;
        }

        public HtmlBuilder Prepend(string selector)
        {
            var elements = _parser.Parse(selector);

            _elements.InsertRange(0, elements);

            return this;
        }

        public HtmlBuilder Replace(string selector)
        {
            var elements = _parser.Parse(selector);

            _elements = elements;

            return this;
        }

        //public HtmlBuilder AppendText(string text)
        //{

        //    return this;
        //}

        //public HtmlBuilder PrependText(string text)
        //{
        //    return this;
        //}

        //public HtmlBuilder ReplaceWithText(string text)
        //{
        //    return this;
        //}

        /// <summary>
        /// Returns the HtmlBuidler as a string, causing all of the elements
        /// too be rendered immediately.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _elements.Aggregate("", (cur, el) => cur + el.ToString());
        }
    }
}
