using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.EliasCoders
{
    internal class DeltaCoder : NumberCoder
    {
        public DeltaCoder()
        {
            _name = "Дельта-код Элиаса";
        }

        public override string Encode(long n)
        {
            if (n == 0)
                throw new ArgumentOutOfRangeException();

            if (n == 1)
                return "1";

            string ans = "";

            string N = ConvertToSystem(n);

            ans += N.Substring(1);

            ans = DelimiterString + ans;

            GammaCoder G = new GammaCoder();

            ans = G.Encode(N.Length) + ans;

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

            int L = (int)ConvertToDecimal(s.Substring(zeros_count, zeros_count + 1));

            string N = "1" + s.Substring(zeros_count + zeros_count + 1, L - 1);

            return ConvertToDecimal(N).Value;
        }
    }
}
