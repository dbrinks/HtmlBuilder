using System.Collections.Generic;

namespace HtmlBuilder.Models
{
    public abstract class Node
    {
        public Node Parent;
        public Node NextSibling;
        public Node PreviousSibling;
        public List<Node> Children;

        protected Node()
        {
            Children = new List<Node>();
        }
    }
}
