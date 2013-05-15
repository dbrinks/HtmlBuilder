using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HtmlBuilder
{

    /// <summary>
    /// 
    /// </summary>
    public class HtmlElement
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<HtmlElement> Children { get; set; }

        /// <summary>
        /// These are just the common tags...  Maybe a setting to overwrite this
        /// </summary>
        private static readonly string[] _selfClosingTags = new[] { "base", "basefont", "frame", 
            "link", "meta", "area", "br", "col", "hr", "img", "input", "param" };

        /// <summary>
        /// 
        /// </summary>
        public HtmlElement()
        {
            Classes = new List<string>();
            Attributes = new Dictionary<string, string>();
            Children = new List<HtmlElement>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

            if (Children != null)
            {
                tag.InnerHtml = Children.Aggregate("", (cur, el) => cur + el.ToString());
            }

            return tag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToTagBuilder().ToString(GetTagRenderMode());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private TagRenderMode GetTagRenderMode()
        {
            return _selfClosingTags.Contains(TagName)
                ? TagRenderMode.SelfClosing
                : TagRenderMode.Normal;
        }
    }
}