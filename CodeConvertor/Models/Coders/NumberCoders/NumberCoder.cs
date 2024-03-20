using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders
{
    internal abstract class NumberCoder : Coder
    {
        private string _delimiterString;

        public string DelimiterString
        {
            get { return _delimiterString; }
            set { _delimiterString = value; }
        }
        public abstract string Encode(long n);

        public override string Encode(string n)
        {
            return Encode(Int64.Parse(n));
        }

        public static char ConvertNumToSymbol(int n)
        {
            if (n <= 9)
                return (char)('0' + n);
            else
                return (char)('A' + n - 10);
        }

        public static string ConvertToSystem(long n, int basis = 2)
        {
            if (n == 0)
                return "0";

            string ans = "";

            while (n != 0)
            {
                ans += ConvertNumToSymbol((int)(n % basis));
                n /= basis;
            }

            return ans.Reverse();
        }

        public static int ConvertSymbolToNum(char c)
        {
            if (c >= '0' && c <= '9')
                return c - '0';
            if (c >= 'A' && c <= 'Z')
                return c - 'A' + 10;
            if (c >= 'a' && c <= 'z')
                return c - 'a' + 10;

            return -1;
        }

        public static long? ConvertToDecimal(string n, int basis = 2)
        {
            long ans = 0;

            for (int i = 0; i < n.Length; i++)
            {
                if (ans >= long.MaxValue / 100)
                    return null;

                ans *= basis;

                int cur = ConvertSymbolToNum(n[i]);

                if (cur >= basis)
                    return null;

                if (cur == -1)
                    return null;

                ans += cur;
            }

            return ans;
        }

        public static string RemoveSeparator(string input, string separator)
        {
            if (separator != "")
                return input.Replace(separator, "");

            return input;
        }

        public static bool CheckOnZerosOnes(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != '0' && s[i] != '1')
                    return false;
            }

            return true;
        }

        public static string ConvertString(string inputString, NumberCoder left, NumberCoder right)
        {
            StringBuilder sb = new StringBuilder(inputString.Length);

            inputString = inputString.Replace("\r", "");

            string[] lines = inputString.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string curLine = lines[i];

                if (curLine == "")
                {
                    sb.Append('\n');
                    continue;
                }

                string outLine;

                long? n;

                try
                {
                    n = left.Decode(curLine);

                    try
                    {
                        outLine = right.Encode(n.Value);
                    }
                    catch
                    {
                        outLine = "Не получилось закодировать: " + curLine;
                    }
                }
                catch
                { 
                    outLine = "Не получилось декодировать: " + curLine;
                }

                sb.Append(outLine);
                sb.Append('\n');
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}
