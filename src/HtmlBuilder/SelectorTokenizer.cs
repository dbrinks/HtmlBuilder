using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public class SelectorTokenizer
    {
        private static readonly char[] _specialCharacters = new[] { '#', '.', '[', ']', '>', '{', '}', '*', '+' };
        private static readonly Regex _idRegex = new Regex(@"#([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _textRegex = new Regex(@"\{(.+)\}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _classRegex = new Regex(@"\.([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _countRegex = new Regex(@"\*\s*[0-9]+", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex _attributeRegex = new Regex(@"\[([^\]~\$\*\^\|\!]+)(=[^\]]+)?\]", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);


    }

    public class SelectorTokens
    {
        public string Tag { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public List<Node> Children { get; set; }

        public List<Node> Siblings { get; set; }
    }
}
