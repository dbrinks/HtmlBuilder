using System.Collections.Generic;

namespace HtmlBuilder.Models
{
    public class SelectorTokens
    {
        public string Tag { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public string ChildSelector { get; set; }

        public string NextSiblingSelector { get; set; }

        public int Count { get; set; }
    }
}
