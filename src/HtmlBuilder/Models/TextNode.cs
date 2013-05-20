namespace HtmlBuilder.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TextNode : Node
    {
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public TextNode(string text)
        {
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text ?? string.Empty;
        }
    }
}
