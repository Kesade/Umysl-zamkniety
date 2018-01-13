using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class StringHelpers
    {
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
