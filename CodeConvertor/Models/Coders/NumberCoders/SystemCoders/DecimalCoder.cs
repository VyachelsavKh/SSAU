﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders.SystemCoders
{
    internal class DecimalCoder : SystemCoder
    {
        public DecimalCoder() 
        {
            _coderDescription = "Десятичное число";
            _basis = 10;
            _delimiterInterval = 3;
        }
    }
}
