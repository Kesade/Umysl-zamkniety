using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class StringHelpers
    {
        public static string HtmlToPlainText(this string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        public static bool IsBalanced(this string input, Dictionary<char, char> bracketPairs)
        {
            Stack<char> brackets = new Stack<char>();

            try
            {
                foreach (char c in input)
                {
                    if (bracketPairs.Keys.Contains(c))
                    {
                        brackets.Push(c);
                    }
                    else if (bracketPairs.Values.Contains(c))
                    {
                        if (c == bracketPairs[brackets.First()])
                        {
                            brackets.Pop();
                        }
                        else
                            return false;
                    }
                }
            }
            catch
            {
                return false;
            }

            return !brackets.Any() ? true : false;
        }

    }
}
