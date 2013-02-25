using System.Collections.Generic;
using System.Web.Mvc;

namespace HtmlBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class Element
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public List<Element> Children { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        public Element()
        {
            Classes = new List<string>();
        }

        public TagBuilder ToTagBuilder()
        {
            var tag = new TagBuilder(!string.IsNullOrEmpty(TagName) ? TagName : "div");

            if (!string.IsNullOrEmpty(Id))
            {
                tag.MergeAttribute("id", Id);
            }

            foreach (var cssClass in Classes)
            {
                tag.AddCssClass(cssClass);
            }

            tag.MergeAttributes(Attributes);

            return tag;
        }

        public override string ToString()
        {
            return ToTagBuilder().ToString();
        }
    }
}
