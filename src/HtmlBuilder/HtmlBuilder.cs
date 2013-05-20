using System;
using System.Collections.Generic;
using System.Linq;
using HtmlBuilder.Extensions;
using HtmlBuilder.Models;

// Settings Ideas...
//
// Self closing tags if possible
// Set self closing tags
// 

namespace HtmlBuilder
{
    public class HtmlBuilder
    {
        private readonly Node _root;
        private Node _current;
        private List<Node> _elements;

        private readonly string _selector;
        private readonly HtmlTreeBuilder _builder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        public HtmlBuilder(string selector)
        {
            _builder = new HtmlTreeBuilder();
            _selector = selector;

            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException();
            }

            _elements = _builder.Build(_selector);

            // Something MUST be returned
            _root = _elements[0];
            _current = _root;
        }


        /// <summary>
        /// Returns the HtmlBuidler as a string, causing all of the elements
        /// too be rendered immediately.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _elements.Aggregate("", (cur, el) => cur + el.ToString());
        }


        #region API



        public HtmlBuilder Parent()
        {
            if (_current.Parent != null)
            {
                _current = _current.Parent;
            }

            return this;
        }

        public HtmlBuilder Previous()
        {
            if (_current.PreviousSibling != null)
            {
                _current = _current.PreviousSibling;
            }

            return this;
        }

        public HtmlBuilder Next()
        {
            if (_current.NextSibling != null)
            {
                _current = _current.NextSibling;
            }

            return this;
        }

        public HtmlBuilder Children()
        {
            if (_current.Children != null)
            {
                _current = _current.Children.First();
            }

            return this;
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

            foreach (var element in _elements.OfType<TagNode>())
            {
                element.Classes = names.Aggregate("", (cur, cls) => cur + " " + cls).Trim();
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
            foreach (var element in _elements.OfType<TagNode>())
            {
                foreach (var kvp in keyValuePairs.Where(pairs => !string.IsNullOrEmpty(pairs.Key)))
                {
                    element.AddAttribute(kvp.Key, kvp.Value);
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
            _current.AsTagNode().Id = id;

            return this;
        }

        public HtmlBuilder AppendChildren(string selector)
        {
            var children = _builder.Build(selector);

            foreach (var el in _elements)
            {
                el.Children.AddRange(children);
            }

            return this;
        }

        public HtmlBuilder PrependChildren(string selector)
        {
            var children = _builder.Build(selector);

            foreach (var el in _elements)
            {
                el.Children.InsertRange(0, children);
            }

            return this;
        }

        public HtmlBuilder ReplaceChildren(string selector)
        {
            var children = _builder.Build(selector);

            foreach (var el in _elements)
            {
                el.Children = new List<Node>(children);
            }

            return this;
        }

        public HtmlBuilder Append(string selector)
        {
            var elements = _builder.Build(selector);

            _elements.AddRange(elements);

            return this;
        }

        public HtmlBuilder Prepend(string selector)
        {
            var elements = _builder.Build(selector);

            _elements.InsertRange(0, elements);

            return this;
        }

        public HtmlBuilder Replace(string selector)
        {
            var elements = _builder.Build(selector);

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
        #endregion
    }
}
