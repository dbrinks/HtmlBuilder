using System.Web.Mvc;

namespace HtmlBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class Element
    {
        public string TagName { get; set; }
        public string ClassName { get; set; }

        public TagBuilder ToTagBuilder()
        {
            var tag = new TagBuilder(!string.IsNullOrEmpty(TagName) ? TagName : "div");

            if (!string.IsNullOrEmpty(ClassName))
            {
                tag.AddCssClass(ClassName);
            }

            return tag;
        }

        public override string ToString()
        {
            return ToTagBuilder().ToString();
        }
    }
}
