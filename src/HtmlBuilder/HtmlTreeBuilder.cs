using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public class HtmlTreeBuilder : IHtmlTreeBuilder
    {
        private readonly ISelectorTokenizer _tokenizer;

        // Disclaimer: I really do not like this... but it works for now..
        public HtmlTreeBuilder()
            : this(new SelectorTokenizer())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenizer"></param>
        public HtmlTreeBuilder(ISelectorTokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public NodeTree Build(string selector)
        {
            return new NodeTree(BuildTree(selector));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="parent"></param>
        /// <param name="prevSibling"></param>
        /// <returns></returns>
        private Node BuildTree(string selector, Node parent = null, Node prevSibling = null)
        {
            var tokens = _tokenizer.Parse(selector);
            Node root = null;
            Node prev = null;

            for (var i = 0; i < tokens.Count; i++)
            {
                var node = CreateNode(tokens);

                if (i == 0)
                {
                    root = node;
                }

                if (prev != null)
                {
                    prev.NextSibling = node;
                    node.PreviousSibling = prev;
                }

                if (parent != null)
                {
                    parent.Children.Add(node);
                    node.Parent = parent;
                }

                if (prevSibling != null)
                {
                    prevSibling.NextSibling = node;
                    node.PreviousSibling = prevSibling;
                }

                if (!string.IsNullOrEmpty(tokens.ChildSelector))
                {
                    BuildTree(tokens.ChildSelector, node);
                }

                // dont want to add a sibling if we dont have to... '*' siblings take precedence over '+' siblings
                if (!string.IsNullOrEmpty(tokens.NextSiblingSelector) && i == tokens.Count - 1)
                {
                    BuildTree(tokens.NextSiblingSelector, prevSibling: node);
                }

                prev = node;
            }

            return root;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private static Node CreateNode(SelectorTokens tokens)
        {
            var node = new TagNode(tokens.Tag);
            node.SetAttributes(tokens.Attributes);
            return node;
        }
    }
}
