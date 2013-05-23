using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlBuilder.Extensions
{
    public static class MatchCollectionExtensions
    {
        public static Dictionary<string, string> ToAttributeDictionary(this MatchCollection matches)
        {
            return matches.Cast<Match>()
                          .Select(m => m.Value.Split('='))
                          .ToDictionary(s => s[0].Trim('['),
                                        s => s.Count() > 1
                                            ? s[1].ReplaceQuotes().Trim(']')
                                            : string.Empty);
        }

        public static string ToCSSClassString(this MatchCollection matches)
        {
            return matches.Cast<Match>()
                          .Aggregate("", (current, match) => current + " " + match.Value.TrimStart('.'))
                          .Trim();
        }

        public static string ToIDString(this MatchCollection matches)
        {
            var match = matches.Cast<Match>().FirstOrDefault();
            return match != null
                       ? match.Value.TrimStart('#')
                       : string.Empty;
        }
    }
}
