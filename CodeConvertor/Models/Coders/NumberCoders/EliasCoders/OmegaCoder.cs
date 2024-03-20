using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.EliasCoders
{
    internal class OmegaCoder : NumberCoder
    {
        public OmegaCoder()
        {
            _name = "Омега-код Элиаса";
        }

        public override string Encode(long n)
        {
            if (n == 0)
                throw new ArgumentOutOfRangeException();

            if (n == 1)
                return "0";

            string ans = DelimiterString + "0";

            string N_str;

            while (n != 1)
            {
                N_str = ConvertToSystem(n);

                ans = N_str + ans;

                ans = DelimiterString + ans;

                n = N_str.Length - 1;
            }

            ans = ans.Substring(DelimiterString.Length);

            return ans;
        }

        public override long Decode(string s)
        {
            s = RemoveSeparator(s, DelimiterString);

            if (!CheckOnZerosOnes(s))
                throw new FormatException();

            long N = 1;

            int i = 0;

            string cur_str;

            while (s[i] == '1')
            {
                cur_str = s.Substring(i, (int)N + 1);

                i += (int)N + 1;

                N = ConvertToDecimal(cur_str).Value;
            }

            return N;
        }
    }
}
