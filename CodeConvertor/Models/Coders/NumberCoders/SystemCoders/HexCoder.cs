using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.SystemCoders
{
    internal class HexCoder : SystemCoder
    {
        public HexCoder()
        {
            _name = "Шестнадцатеричное число";
            _basis = 16;
        }
    }
}
