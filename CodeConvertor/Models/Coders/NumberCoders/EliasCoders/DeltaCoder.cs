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

        public override FunctionResult<string> Encode(ulong n)
        {
            if (n == 0)
                return new FunctionResult<string>("", "Не получилось закодировать: 0");

            if (n == 1)
                return new FunctionResult<string>("1");

            string ans = "";

            string N = ConvertToSystem(n);

            ans += N.Substring(1);

            ans = DelimiterString + ans;

            GammaCoder G = new GammaCoder();

            ans = G.Encode((ulong)N.Length).Result + ans;

            return new FunctionResult<string>(ans);
        }

        public override FunctionResult<ulong> DecodeToDecimal(string s)
        {
            s = s.RemoveSeparator(DelimiterString);

            if (!s.CheckOnZerosOnes())
                return new FunctionResult<ulong>(0, "Не получилось декодировать, есть неизвестные символы: " + s);

            int M = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    M++;
                else
                    break;
            }

            if (s.Length < M + M + 1)
                return new FunctionResult<ulong>(0, "Не получилось декодировать часть с L: " + s);

            FunctionResult<ulong> L = ConvertToDecimal(s.Substring(M, M + 1));

            int L_int = (int)L.Result;

            if (s.Length < M + M + 1 + L_int - 1)
                return new FunctionResult<ulong>(0, "Не получилось декодировать число: " + s);

            string N = "1" + s.Substring(M + M + 1, L_int - 1);

            ulong ans = ConvertToDecimal(N).Result;

            return new FunctionResult<ulong>(ans);
        }
    }
}
