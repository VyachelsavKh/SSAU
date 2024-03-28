using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.SystemCoders
{
    internal class OctCoder : SystemCoder
    {
        public OctCoder()
        {
            _coderDescription = "Восьмеричное число";
            _basis = 8;
            _delimiterInterval = 3;
        }
    }
}
