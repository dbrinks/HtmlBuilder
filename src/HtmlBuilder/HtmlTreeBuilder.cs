using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public class HtmlTreeBuilder
    {
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

            var tokens = SelectorTokenizer.Parse(selector);

            for (var i = 0; i < tokens.Count; i++)
            {
                var element = CreateElement(tokens);

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
        /// <param name="tokens"></param>
        /// <returns></returns>
        private static TagNode CreateElement(SelectorTokens tokens)
        {
            var node = new TagNode(tokens.Tag);
            node.SetAttributes(tokens.Attributes);
            return node;
        }
    }
}
