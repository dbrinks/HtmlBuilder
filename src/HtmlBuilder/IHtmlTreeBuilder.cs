using HtmlBuilder.Models;

namespace HtmlBuilder
{
    public interface IHtmlTreeBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        NodeTree Build(string selector);
    }
}