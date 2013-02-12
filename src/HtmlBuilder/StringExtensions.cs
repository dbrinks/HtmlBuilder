using System.Linq;

namespace HtmlBuilder
{
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string SubstringUntil(this string str, int index, char character)
        {
            return str.SubstringUntil(index, new[] { character });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static string SubstringUntil(this string str, int index, string characters)
        {
            return str.SubstringUntil(index, characters.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static string SubstringUntil(this string str, int index, char[] characters)
        {
            var end = -1;
            for (var i = index; i < str.Length; i++)
            {
                if (characters.Contains(str[i]))
                {
                    end = i - index;
                    break;
                }
            }
            return end != -1
                ? str.Substring(index, end)
                : str.Substring(index, str.Length - index);
        }
    }
}
