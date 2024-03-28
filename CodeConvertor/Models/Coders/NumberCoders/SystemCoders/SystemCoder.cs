using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.SystemCoders
{
    internal class SystemCoder : NumberCoder
    {
        protected uint _basis;
        protected int _delimiterInterval;

        public SystemCoder()
        {
            _coderDescription = "Десятичное число";
            _basis = 10;
            _delimiterInterval = 3;
        }

        public SystemCoder(string name, uint basis)
        {
            _coderDescription = name;
            _basis = basis;
        }

        public override CoderResult<ulong> DecodeToDecimal(string s)
        {
            s = s.RemoveSeparator(DelimiterString);

            if (s == "")
                return new CoderResult<ulong>(0, "Не получилось декодировать: " + s);

            ulong? result = ConvertToDecimal(s, _basis);

            CoderResult<ulong> res;

            if (result == null)
            {
                res = new CoderResult<ulong>(0, "Не получилось декодировать: " + s);
            }
            else
            {
                res = new CoderResult<ulong>(result.Value);
            }

            return res;
        }

        public override CoderResult<string> Encode(ulong n)
        {
            string result = ConvertToSystem(n, _basis);

            result = _insertDelimiter(result);

            CoderResult<string> res = new CoderResult<string>(result);

            return res;
        }

        private string _insertDelimiter(string original)
        {
            if (original.Length <= _delimiterInterval)
                return original;

            for (int i = original.Length - _delimiterInterval; i > 0; i -= _delimiterInterval)
            {
                original = original.Insert(i, DelimiterString);
            }

            return original;
        }
    }
}
