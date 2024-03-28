using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models
{
    internal static class StringExtensions
    {
        public static string Repeat(this string value, int count) => string.Concat(Enumerable.Repeat(value, count));
        public static string Reverse(this string str)
        {
            StringBuilder reversed = new StringBuilder(str.Length);

            for (int i = str.Length - 1; i >= 0; i--)
            {
                reversed.Append(str[i]);
            }

            return reversed.ToString();

        }

        public static string RemoveSeparator(this string input, string separator)
        {
            if (separator != "")
                return input.Replace(separator, "");

            return input;
        }

        public static Dictionary<char, int> GetCharacterFrequencies(this string input)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>(100);

            foreach (char c in input)
            {
                if (frequencies.ContainsKey(c))
                {
                    frequencies[c]++;
                }
                else
                {
                    frequencies[c] = 1;
                }
            }

            var orderedFrequencies = frequencies.OrderByDescending(x => x.Value);

            return orderedFrequencies.ToDictionary(x => x.Key, x => x.Value);
        }

        public static string[] GetLines(this string s)
        {
            return s.Split('\n');
        }

        public static bool CheckOnZerosOnes(this string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != '0' && s[i] != '1')
                    return false;
            }

            return true;
        }
    }
}
