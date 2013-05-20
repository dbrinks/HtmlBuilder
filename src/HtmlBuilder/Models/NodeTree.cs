using System;

namespace HtmlBuilder.Models
{
    public class NodeTree
    {
        public Node Root { get; private set; }

        public Node Current { get; private set; }

        public NodeTree(Node node)
        {
            if (node == null)
            {
                // need a root to start from...
                throw new ArgumentNullException();
            }

            Root = node;
            Current = node;
        }

        public bool Next()
        {
            var result = true;

            if (Current.NextSibling != null)
            {
                Current = Current.NextSibling;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool Previous()
        {
            var result = true;

            if (Current.PreviousSibling != null)
            {
                Current = Current.PreviousSibling;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool Parent()
        {
            var result = true;

            if (Current.Parent != null)
            {
                Current = Current.Parent;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool FirstChild()
        {
            var result = true;

            if (Current.Children != null && Current.Children.Count > 0)
            {
                Current = Current.Children[0];
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool LastChild()
        {
            var result = true;

            if (Current.Children != null && Current.Children.Count > 0)
            {
                Current = Current.Children[Current.Children.Count - 1];
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
