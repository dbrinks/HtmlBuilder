using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HtmlBuilder.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TagNode : Node
    {
        public string TagName { get; private set; }
        private Dictionary<string, string> _attributes;

        /// <summary>
        /// These are just the common tags...  
        /// 
        /// Maybe a setting to overwrite this would be nice...
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
            set { _attributes["class"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { _attributes["id"] = value; }
        }

        //public string Dataset
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        public TagNode(string tagName)
        {
            TagName = tagName;
            _attributes = new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAttribute(string key)
        {
            string value;
            _attributes.TryGetValue(key, out value);

            return !string.IsNullOrEmpty(value)
                ? value
                : string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        public void SetAttributes(Dictionary<string, string> attributes)
        {
            _attributes = attributes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetAttribute(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException();
            }

            _attributes[key] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddAttribute(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException();
            }

            var current = GetAttribute(key);
            _attributes[key] = (current + " " + value).Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetData(string key)
        {
            return GetAttribute("data-" + key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void AddData(string key, string value)
        {
            AddAttribute("data-" + key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TagBuilder ToTagBuilder()
        {
            var name = !string.IsNullOrEmpty(TagName) ? TagName : "div";
            var tag = new TagBuilder(name);

            tag.MergeAttributes(_attributes);

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