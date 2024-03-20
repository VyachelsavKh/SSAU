using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders
{
    internal abstract class Coder
    {
        protected string _name;

        public abstract string Encode(string n);

        public abstract long Decode(string s);

        public override string ToString()
        {
            return _name;
        }
    }
}
