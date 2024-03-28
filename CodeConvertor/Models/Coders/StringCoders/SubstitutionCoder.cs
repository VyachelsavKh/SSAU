using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.StringCoders
{
    internal abstract class SubstitutionCoder : StringCoder
    {
        protected static (FunctionResult<Dictionary<char, string>>, string decodeString, string remainingString) ReadCodingDictionary(string s)
        {
            string[] InputLines = s.Split('\n');

            int codesCount;

            string[] lines = new string[InputLines.Length];

            for (int i = 0, j = 0; i < InputLines.Length; i++)
                if (InputLines[i].Length != 0)
                    lines[j++] = InputLines[i];

            try
            {
                codesCount = Int32.Parse(lines[0]);
            }
            catch
            {
                return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать количество строк в таблице кодов"), null, null);
            }

            if (lines.Length < codesCount + 1)
                return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать таблицу кодов, мало строк кодов"),null, null);

            Dictionary<char, string> codingDictionary = new Dictionary<char, string>(codesCount);

            for (int i = 1; i <= codesCount; i++)
            {
                if (lines[i].Length < 3)
                    return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать элемент таблицы кодов, нет кода: " + lines[i]), null, null);

                if (lines[i][1] != ' ')
                    return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать элемент таблицы кодов, неправильный разделитель: " + lines[i]), null, null);


                char symbol = lines[i][0];
                string code = lines[i].Substring(2);

                if (!code.CheckOnZerosOnes())
                    return (new FunctionResult<Dictionary<char, string>>(null, "Не получилось считать элемент таблицы кодов, в коде есть неизвестные символы: " + lines[i]), null, null);

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
