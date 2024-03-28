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
            _coderDescription = "Омега-код Элиаса";
        }

        public override CoderResult<string> Encode(ulong n)
        {
            if (n == 0)
                return new CoderResult<string>("", "Не получилось закодировать: 0");

            if (n == 1)
                return new CoderResult<string>("1");

            string ans = DelimiterString + "0";

            string N_str;

            while (n != 1)
            {
                N_str = ConvertToSystem(n);

                ans = N_str + ans;

                ans = DelimiterString + ans;

                n = (ulong)N_str.Length - 1;
            }

            ans = ans.Substring(DelimiterString.Length);

            return new CoderResult<string>(ans);
        }

        public override CoderResult<ulong> DecodeToDecimal(string s)
        {
            s = s.RemoveSeparator(DelimiterString);

            if (!CheckOnZerosOnes(s))
                throw new FormatException();

            ulong N = 1;

            int i = 0;

            string cur_str;

            try
            {

                while (s[i] == '1')
                {
                    cur_str = s.Substring(i, (int)N + 1);

                    i += (int)N + 1;

                    N = ConvertToDecimal(cur_str).Value;
                }

                return new CoderResult<ulong>(N);
            }
            catch
            {
                return new CoderResult<ulong>(0, "Не получилось декодировать: " + s);
            }
        }
    }
}
