using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public interface ISelectorTokenizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        SelectorTokens Parse(string selector);
    }
}