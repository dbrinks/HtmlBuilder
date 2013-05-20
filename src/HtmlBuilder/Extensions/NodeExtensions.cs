using System;
using HtmlBuilder.Models;

namespace HtmlBuilder.Extensions
{
    public static class NodeExtensions
    {
        public static TagNode AsTagNode(this Node node)
        {
            var tagNode = node as TagNode;
            if (tagNode == null)
            {
                throw new InvalidCastException();
            }
            return tagNode;
        }

        public static TextNode AsTextNode(this Node node)
        {
            var textNode = node as TextNode;
            if (textNode == null)
            {
                throw new InvalidCastException();
            }
            return textNode;
        }
    }
}
