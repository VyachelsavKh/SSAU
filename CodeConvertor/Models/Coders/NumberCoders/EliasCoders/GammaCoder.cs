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
            _coderDescription = "Гамма-код Элиаса";
        }

        public override CoderResult<string> Encode(ulong n)
        {
            if (n == 0)
                return new CoderResult<string>("", "Не получилось закодировать: 0");

            if (n == 1)
                return new CoderResult<string>("1");

            string ans = "";

            ans += ConvertToSystem(n);

            string zeros = "0".Repeat(ans.Length - 1);

            ans = DelimiterString + ans;

            ans = zeros + ans;

            return new CoderResult<string>(ans);
        }

        public override CoderResult<ulong> DecodeToDecimal(string s)
        {
            s = s.RemoveSeparator(DelimiterString);

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

            if (s.Length != zeros_count * 2 + 1)
                return new CoderResult<ulong>(0, "Не получилось декодировать: " + s);

            ulong ans = ConvertToDecimal(s.Substring(zeros_count, zeros_count + 1)).Value;

            return new CoderResult<ulong>(ans);
        }
    }
}
