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
            _name = "Восьмеричное число";
            _basis = 8;
        }
    }
}
