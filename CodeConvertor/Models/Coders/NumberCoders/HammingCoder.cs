using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders
{
    internal class HammingCoder : NumberCoder
    {
        public HammingCoder()
        {
            _coderDescription = "Код Хэмминга";
        }

        private int FindControlBitsCount(int k)
        {
            int n = 2;

            while (!(n + k + 1 <= 1 << n))
                n++;

            return n;
        }

        private string AddCheckBits(string n)
        {
            if (n == null)
                return null;

            int controlBitsCount = FindControlBitsCount(n.Length);

            StringBuilder sb = new StringBuilder(n.Length + controlBitsCount + 10);

            sb.Append('0', n.Length + controlBitsCount + 1);

            sb[0] = '_';

            int nBitPos = 0;
            int controlBitPos = 1;

            for (int i = 1; nBitPos < n.Length; i++)
            {
                if (i == controlBitPos)
                {
                    controlBitPos <<= 1;
                }
                else
                {
                    sb[i] = n[nBitPos];
                    nBitPos++;
                }
            }

            return sb.ToString();
        }

        private int FindControlBitSum(string s, int controlBitPos)
        {
            int ans = 0;

            for (int i = controlBitPos; i < s.Length; i++)
            {
                for (int j = 0; i < s.Length && j < controlBitPos; j++, i++)
                {
                    ans += s[i] - '0';
                }

                i += controlBitPos - 1;
            }

            return ans;
        }

        public override FunctionResult<string> Encode(string n)
        {
            n = n.RemoveSeparator(DelimiterString);

            if (!n.CheckOnZerosOnes())
                throw new ArgumentException();

            StringBuilder s = new StringBuilder(AddCheckBits(n));

            int controlBit = 1;

            for (; controlBit < s.Length; controlBit <<= 1)
            {
                int sum = FindControlBitSum(s.ToString(), controlBit);

                s[controlBit] = (char)('0' + (sum & 1));
            }

            if (DelimiterString != "")
            {
                StringBuilder new_s = new StringBuilder(s.Length + (int)Math.Log(s.Length, 2) + 10);

                new_s.Append("_(");
                new_s.Append(s[1]);
                new_s.Append(s[2]);
                new_s.Append(')');
                new_s.Append(s[3]);

                int curControlBitPos = 4;

                for (int i = 4; i < s.Length; i++)
                {
                    if (i == curControlBitPos)
                    {
                        curControlBitPos <<= 1;
                        new_s.Append('(');
                        new_s.Append(s[i]);
                        new_s.Append(')');
                    }
                    else
                        new_s.Append(s[i]);
                }

                s = new_s;
            }

            s.Remove(0, 1);

            return new FunctionResult<string>(s.ToString());
        }

        public override FunctionResult<string> Encode(ulong n)
        {
            return Encode(ConvertToSystem(n));
        }

        public override FunctionResult<string> Decode(string s)
        {
            if (DelimiterString != "")
            {
                s = s.RemoveSeparator("(");
                s = s.RemoveSeparator(")");
            }
            if (!s.CheckOnZerosOnes())
                return new FunctionResult<string>("", "Не получилось декодировать: " + s);

            string workWith = '_' + s;

            int errorControlBitsSum = 0;

            int controlBit = 1;

            for (; controlBit < workWith.Length; controlBit <<= 1)
            {
                int sum = FindControlBitSum(workWith, controlBit);

                if (sum % 2 != 0)
                {
                    errorControlBitsSum += controlBit;
                }
            }

            if (errorControlBitsSum > workWith.Length)
                return new FunctionResult<string>("", "Не получилось декодировать: " + s);

            StringBuilder str = new StringBuilder(workWith.Length);

            controlBit = 1;

            for (int i = 1; i < workWith.Length; i++)
            {
                if (i == controlBit)
                    controlBit <<= 1;
                else
                {
                    if (i == errorControlBitsSum)
                    {
                        if (workWith[i] == '0')
                            str.Append('1');
                        else
                            str.Append('0');
                    }
                    else
                        str.Append(workWith[i]);
                }
            }

            if (errorControlBitsSum != 0)
            {
                return new FunctionResult<string>(str.ToString(), "Ошибка в " + errorControlBitsSum + " бите: " + s, 2);
            }

            return new FunctionResult<string>(str.ToString());
        }

        public override FunctionResult<ulong> DecodeToDecimal(string n)
        {
            FunctionResult<string> res = Decode(n);

            ulong result = 0;

            if (res.IsOk() || res.ErrorCode == 2)
            {
                result = ConvertToDecimal(res.Result).Result;
            }

            return new FunctionResult<ulong>(result, res.Error, res.ErrorCode);
        }
    }
}
