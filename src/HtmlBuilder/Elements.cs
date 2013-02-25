using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlBuilder
{
    public class Elements
    {
        private readonly List<Elements> _elements;

        /// <summary>
        /// 
        /// </summary>
        public Elements()
        {
            _elements = new List<Elements>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        public Elements(List<Elements> elements)
        {
            _elements = elements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        public void Add(Elements el)
        {
            _elements.Add(el);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ""; // TODO implement
        }
    }
}
