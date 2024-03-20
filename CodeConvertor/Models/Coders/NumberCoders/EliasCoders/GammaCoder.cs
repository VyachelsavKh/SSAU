using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.EliasCoders
{
    internal class GammaCoder : NumberCoder
    {
        public GammaCoder()
        {
            _name = "Гамма-код Элиаса";
        }

        public override string Encode(long n)
        {
            if (n == 0)
                throw new ArgumentOutOfRangeException();

            if (n == 1)
                return "1";

            string ans = "";

            ans += ConvertToSystem(n);

            string zeros = "0".Repeat(ans.Length - 1);

            ans = DelimiterString + ans;

            ans = zeros + ans;

            return ans;
        }

        public override long Decode(string s)
        {
            s = RemoveSeparator(s, DelimiterString);

            if (!CheckOnZerosOnes(s))
                throw new FormatException();

            int zeros_count = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    zeros_count++;
                else
                    break;
            }

            return ConvertToDecimal(s.Substring(zeros_count, zeros_count + 1)).Value;
        }
    }
}
