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

        public override FunctionResult<ulong> DecodeToDecimal(string s)
        {
            s = s.RemoveSeparator(DelimiterString);

            if (s == "")
                return new FunctionResult<ulong>(0, "Не получилось декодировать: " + s);

            return ConvertToDecimal(s, _basis);
        }

        public override FunctionResult<string> Encode(ulong n)
        {
            string result = ConvertToSystem(n, _basis);

            result = _insertDelimiter(result);

            FunctionResult<string> res = new FunctionResult<string>(result);

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
