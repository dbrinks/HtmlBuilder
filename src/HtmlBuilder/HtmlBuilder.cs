using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HtmlBuilder
{
    public class HtmlBuilder
    {
        public string Selector { get; set; }
        public List<HtmlBuilder> InnerHtml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HtmlBuilder()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        public HtmlBuilder(string selector)
        {

        }

        public override string ToString()
        {
            return "";
        }
    }

    public class SelectorParser
    {

    }
}
