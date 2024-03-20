using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.SystemCoders
{
    internal class SystemCoder : NumberCoder
    {
        protected int _basis;

        public SystemCoder()
        {
            _name = "Десятичное число";
            _basis = 10;
        }

        public SystemCoder(string name, int basis)
        {
            _name = name;
            _basis = basis;
        }

        public override long Decode(string s)
        {
            return ConvertToDecimal(s, _basis).Value;
        }

        public override string Encode(long n)
        {
            return ConvertToSystem(n, _basis);
        }
    }
}
