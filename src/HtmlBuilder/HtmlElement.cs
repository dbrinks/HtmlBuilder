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
        /// <returns></returns>
        public string Classes
        {
            get { return GetAttribute("class"); }
            set { Attributes["class"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { Attributes["id"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HtmlElement()
        {
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
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetAttribute(string key)
        {
            string value;
            Attributes.TryGetValue(key, out value);

            return !string.IsNullOrEmpty(value)
                ? value
                : string.Empty;
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