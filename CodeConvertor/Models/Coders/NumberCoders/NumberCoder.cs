using CodeConvertor.Models.Coders.NumberCoders.SystemCoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.NumberCoders
{
    internal abstract class NumberCoder : Coder
    {
        public abstract FunctionResult<string> Encode(ulong n);

        public override FunctionResult<string> Encode(string n)
        {
            if (n == "")
                return new FunctionResult<string>("");

            return Encode(UInt64.Parse(n));
        }

        public abstract FunctionResult<ulong> DecodeToDecimal(string n);

        public override FunctionResult<string> Decode(string s)
        {
            if (s == "")
                return new FunctionResult<string>("");

            FunctionResult<ulong> decodeResult = DecodeToDecimal(s);

            FunctionResult<string> result = new FunctionResult<string>(decodeResult.Result.ToString(), decodeResult.Error);

            return result;
        }

        protected static char ConvertNumToSymbol(int n)
        {
            if (n <= 9)
                return (char)('0' + n);
            else
                return (char)('A' + n - 10);
        }

        public static string ConvertToSystem(ulong n, uint basis = 2)
        {
            if (n == 0)
                return "0";

            string ans = "";

            while (n != 0)
            {
                ans += ConvertNumToSymbol((int)(n % basis));
                n /= basis;
            }

            return ans.Reverse();
        }

        protected static int ConvertSymbolToNum(char c)
        {
            if (c >= '0' && c <= '9')
                return c - '0';
            if (c >= 'A' && c <= 'Z')
                return c - 'A' + 10;
            if (c >= 'a' && c <= 'z')
                return c - 'a' + 10;

            return -1;
        }

        public static FunctionResult<ulong> ConvertToDecimal(string n, uint basis = 2)
        {
            if (n == null) 
                return new FunctionResult<ulong>(0, "Не получилось декодировать null");

            ulong ans = 0;

            for (int i = 0; i < n.Length; i++)
            {
                if (ans >= long.MaxValue / 30)
                    return new FunctionResult<ulong>(0, "Не получилось декодировать, число слишком большое: " + n);

                ans *= basis;

                int cur = ConvertSymbolToNum(n[i]);

                if (cur >= basis || cur == -1)
                    return new FunctionResult<ulong>(0, "Не получилось декодировать, есть неизвестные символы: " + n);

                ans += (ulong)cur;
            }

            return new FunctionResult<ulong>(ans);
        }

        public static (string inputErrors, string outputResult, string outputErrors) TranslateString(string inputString, NumberCoder inputCoder, NumberCoder outputCoder)
        {
            string[] inputLines = inputString.GetLines();

            StringBuilder inputErrors = new StringBuilder();
            StringBuilder outputResults = new StringBuilder();
            StringBuilder outputErrors = new StringBuilder();

            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i] == "")
                {
                    outputResults.Append("\n");
                    continue;
                }

                if (inputCoder is BinCoder && outputCoder is HammingCoder)
                {
                    FunctionResult<ulong> inputRes = inputCoder.DecodeToDecimal(inputLines[i]);

                    if (inputRes.IsOk())
                    {
                        FunctionResult<string> outputRes = outputCoder.Encode(inputLines[i]);

                        if (!outputRes.IsOk())
                        {
                            outputErrors.Append(outputRes.Error + "\n");
                        }

                        outputResults.Append(outputRes.Result + "\n");
                    }
                    else
                    {
                        outputResults.Append("\n");
                        inputErrors.Append(inputRes.Error + "\n");
                    }
                }
                else if (inputCoder is HammingCoder && outputCoder is BinCoder)
                {
                    FunctionResult<string> inputRes = inputCoder.Decode(inputLines[i]);

                    if (!inputRes.IsOk())
                    {
                        inputErrors.Append(inputRes.Error + "\n");
                    }

                    outputResults.Append(inputRes.Result + "\n");
                }
                else
                {
                    FunctionResult<ulong> inputRes = inputCoder.DecodeToDecimal(inputLines[i]);

                    if (inputRes.IsOk())
                    {
                        FunctionResult<string> outputRes = outputCoder.Encode(inputRes.Result);

                        if (!outputRes.IsOk())
                        {
                            outputErrors.Append(outputRes.Error + "\n");
                        }

                        outputResults.Append(outputRes.Result + "\n");
                    }
                    else
                    {
                        outputResults.Append("\n");
                        inputErrors.Append(inputRes.Error + "\n");
                    }
                }
            }

            if (inputErrors.Length > 1)
                inputErrors.Remove(inputErrors.Length - 1, 1);

            if (outputResults.Length > 1)
                outputResults.Remove(outputResults.Length - 1, 1);

            if (outputErrors.Length > 1)
                outputErrors.Remove(outputErrors.Length - 1, 1);

            return (inputErrors.ToString(), outputResults.ToString(), outputErrors.ToString());
        }
    }
}