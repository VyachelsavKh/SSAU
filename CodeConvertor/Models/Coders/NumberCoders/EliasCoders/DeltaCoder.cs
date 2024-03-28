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
            _coderDescription = "Дельта-код Элиаса";
        }

        public override CoderResult<string> Encode(ulong n)
        {
            if (n == 0)
                return new CoderResult<string>("", "Не получилось закодировать: 0");

            if (n == 1)
                return new CoderResult<string>("1");

            string ans = "";

            string N = ConvertToSystem(n);

            ans += N.Substring(1);

            ans = DelimiterString + ans;

            GammaCoder G = new GammaCoder();

            ans = G.Encode((ulong)N.Length).Result + ans;

            return new CoderResult<string>(ans);
        }

        public override CoderResult<ulong> DecodeToDecimal(string s)
        {
            s = s.RemoveSeparator(DelimiterString);

            if (!CheckOnZerosOnes(s))
                return new CoderResult<ulong>(0, "Не получилось декодировать: " + s);

            int zeros_count = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    zeros_count++;
                else
                    break;
            }

            try
            {
                int L = (int)ConvertToDecimal(s.Substring(zeros_count, zeros_count + 1));

                string N = "1" + s.Substring(zeros_count + zeros_count + 1, L - 1);

                ulong ans = ConvertToDecimal(N).Value;

                return new CoderResult<ulong>(ans);
            }
            catch
            {
                return new CoderResult<ulong>(0, "Не получилось декодировать: " + s);
            }
        }
    }
}
