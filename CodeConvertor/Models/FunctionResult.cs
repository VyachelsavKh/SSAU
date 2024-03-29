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
        private int _errorCode;

        public FunctionResult(resultType result = default)
        {
            _result = result;
            _error = "";
            _errorCode = 0;
        }

        public FunctionResult(resultType result, string error)
        {
            _result = result;
            _error = error;

            if (error == "")
                _errorCode = 0;
            else
                _errorCode = 1;
        }

        public FunctionResult(resultType result, string error, int errorCode)
        {
            _result = result;
            _error = error;

            if (error == "")
                _errorCode = 0;
            else
                _errorCode = errorCode;
        }

        public resultType Result { get { return _result; } }

        public string Error { get { return _error; } }

        public int ErrorCode { get { return _errorCode; } }

        public bool IsOk() { return _errorCode == 0; }
    }
}
