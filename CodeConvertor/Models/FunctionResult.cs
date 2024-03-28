using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders
{
    internal class FunctionResult<resultType>
    {
        private resultType _result;
        private string _error;
        private bool _ok;

        public FunctionResult(resultType result = default)
        {
            _result = result;
            _error = "";
            _ok = true;
        }

        public FunctionResult(resultType result, string error)
        {
            _result = result;
            _error = error;

            if (error == "")
                _ok = true;
            else
                _ok = false;
        }

        public resultType Result { get { return _result; } }

        public string Error { get { return _error; } }

        public bool IsOk() { return _ok; }
    }
}
