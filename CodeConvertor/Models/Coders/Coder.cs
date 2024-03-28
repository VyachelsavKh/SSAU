using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders
{
    internal abstract class Coder
    {
        protected string _coderDescription;

        private string _delimiterString;

        public Coder() { }

        public Coder(Coder other)
        {
            _delimiterString = other.DelimiterString;
        }

        public string DelimiterString
        {
            get { return _delimiterString; }
            set { _delimiterString = value; }
        }

        public abstract FunctionResult<string> Encode(string n);

        public abstract FunctionResult<string> Decode(string s);

        public override string ToString()
        {
            return _coderDescription;
        }
    }
}
