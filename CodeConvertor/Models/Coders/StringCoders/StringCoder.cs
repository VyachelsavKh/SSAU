using CodeConvertor.Models.Coders.NumberCoders.SystemCoders;
using CodeConvertor.Models.Coders.NumberCoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeConvertor.Models.Coders.StringCoders.UnequalCoders;
using System.Windows.Shapes;
//using CodeConvertor.Models.Coders.StringCoders.UnequalCoders;

namespace CodeConvertor.Models.Coders.StringCoders
{
    internal abstract class StringCoder : Coder
    {
        protected static string ConcatStrings(string[] lines, int startIndex, int linesCount)
        {
            if (startIndex >= lines.Length)
                return "";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < linesCount; i++)
                if (lines[startIndex + i] != null)
                    sb.Append(lines[startIndex + i] + "\n");

            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        protected static string ConcatStrings(string[] lines, int startIndex = 0)
        {
            return ConcatStrings(lines, startIndex, lines.Length - startIndex);
        }

        public static (string inputErrors, string outputResult, string outputErrors) TranslateString(string inputString, StringCoder inputCoder, StringCoder outputCoder)
        {
            StringBuilder inputErrors = new StringBuilder();
            StringBuilder outputResults = new StringBuilder();
            StringBuilder outputErrors = new StringBuilder();

            inputString = inputString.Replace("\r", "");

            if (inputCoder is SimpleString)
            {
                if (outputCoder is SubstitutionCoder)
                {
                    FunctionResult<string> outputRes = outputCoder.Encode(inputString);

                    if (!outputRes.IsOk())
                    {
                        outputErrors.Append(outputRes.Error + "\n");
                    }

                    outputResults.Append(outputRes.Result + "\n");
                }
                else
                {
                    string[] inputLines = inputString.GetLines();

                    for (int i = 0; i < inputLines.Length; i++)
                    {
                        if (inputLines[i] == "")
                        {
                            outputResults.Append("\n");
                            continue;
                        }

                        FunctionResult<string> outputRes = outputCoder.Encode(inputLines[i]);

                        if (!outputRes.IsOk())
                        {
                            outputErrors.Append(outputRes.Error + "\n");
                        }

                        outputResults.Append(outputRes.Result + "\n");
                    }
                }
            }
            else
            {
                inputString = inputString.Replace("\r", "");

                string[] lines = inputString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                string toDecode = ConcatStrings(lines);

                FunctionResult<string> inputRes = inputCoder.Decode(toDecode);

                if (!inputRes.IsOk())
                {
                    outputResults.Append(inputRes.Result);
                    inputErrors.Append(inputRes.Error + "\n");
                }
                else
                {
                    (string inputE, string outputR, string outputE) = TranslateString(inputRes.Result, new SimpleString(), outputCoder);

                    inputErrors.Append(inputE + "\n");
                    outputResults.Append(outputR);
                    outputErrors.Append(outputE + "\n");
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

        protected static (FunctionResult<Dictionary<char, string>>, string decodeString, string remainingString) ReadCodingDictionary(string s)
        {
            string[] lines = s.GetLines();

            int codesCount;

            try
            {
                codesCount = Int32.Parse(lines[0]);
            }
            catch
            {
                return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать количество строк в таблице кодов"), null, null);
            }

            if (codesCount < 0)
                return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать количество строк в таблице кодов"), null, null);

            if (lines.Length < codesCount + 1)
                return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать таблицу кодов, мало строк кодов"), null, null);

            Dictionary<char, string> codingDictionary = new Dictionary<char, string>(codesCount);

            for (int i = 1; i <= codesCount; i++)
            {
                if (lines[i].Length < 3)
                    return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать элемент таблицы кодов, нет кода: " + lines[i]), null, null);

                if (lines[i][1] != ' ')
                    return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать элемент таблицы кодов, неправильный разделитель: " + lines[i]), null, null);


                char symbol = lines[i][0];
                string code = lines[i].Substring(2);

                if (codingDictionary.ContainsKey(symbol))
                    return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать элемент таблицы кодов, повторяющийся символ: " + lines[i]), null, null);

                codingDictionary.Add(symbol, code);
            }

            string decodeString, remainingString;

            if (lines.Length < codesCount + 2)
                return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать строку для декодирования: "), null, null);

            decodeString = lines[codesCount + 1];

            remainingString = ConcatStrings(lines, codesCount + 2);

            return (new FunctionResult<Dictionary<char, string>>(codingDictionary), decodeString, remainingString);
        }
    }
}