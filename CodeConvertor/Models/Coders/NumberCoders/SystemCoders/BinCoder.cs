using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.SystemCoders
{
    internal class BinCoder : SystemCoder
    {
        public BinCoder()
        {
            _coderDescription = "Двоичное число";
            _basis = 2;
            _delimiterInterval = 4;
        }
    }
}
