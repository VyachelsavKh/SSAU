using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.StringCoders
{
    internal class SimpleString : StringCoder
    {
        public SimpleString()
        {
            _coderDescription = "Обычная строка";
        }

        public SimpleString(SimpleString other)
        {
            _coderDescription = "Обычная строка";

            DelimiterString = other.DelimiterString;
        }

        public override FunctionResult<string> Decode(string s)
        {
            return new FunctionResult<string>(s);
        }

        public override FunctionResult<string> Encode(string s)
        {
            return new FunctionResult<string>(s);
        }

    }
}
