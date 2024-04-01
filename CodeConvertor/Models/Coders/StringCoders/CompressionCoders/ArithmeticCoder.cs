using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeConvertor.Models.Coders.StringCoders.CompressionCoders
{
    internal class ArithmeticCoder : StringCoder
    {
        public ArithmeticCoder()
        {
            _coderDescription = "Арифметическое кодирование";
        }

        public override FunctionResult<string> Encode(string s)
        {
            Dictionary<char, int> frequencies = s.GetCharacterFrequencies();

            Dictionary<char, (double, double)> ranges = new Dictionary<char, (double, double)>();

            int sum = 0;

            foreach (int v in frequencies.Values) sum += v;

            double curRange = 0;
            double newRange;

            StringBuilder ans = new StringBuilder();

            foreach (var kvp in frequencies)
            {
                newRange = curRange + kvp.Value / (double)sum;
                ranges[kvp.Key] = (curRange, newRange);
                curRange = newRange;

                ans.Append(kvp.Key + " " + ranges[kvp.Key].Item2 + "\n");
            }

            double min = 0;
            double max = 1;

            StringBuilder steps = new StringBuilder();

            foreach (char c in s)
            {
                double delta = max - min;

                max = min + delta * ranges[c].Item2;
                min = min + delta * ranges[c].Item1;

                steps.Append(c + " ");
                steps.Append(delta + " ");
                steps.Append(min + " ");
                steps.Append(max + "\n");
            }

            ans.Append(s.Length + " ");

            ans.Append(((min + max) / 2).ToString());

            //ans.Append("\n\n" + steps);

            return new FunctionResult<string>(ans.ToString());
        }

        public override FunctionResult<string> Decode(string s)
        {
            if (s == "")
                return new FunctionResult<string>(s);

            (FunctionResult<Dictionary<char, (double, double)>> rangeResult, int stepsCount, double decodeValue, string remainingString) = ReadRangeTable(s);

            if (!rangeResult.IsOk())
                return new FunctionResult<string>("", rangeResult.Error);

            Dictionary<char, (double, double)> ranges = rangeResult.Result;

            StringBuilder ans = new StringBuilder();

            for(int i = 0; i < stepsCount; i++)
            {
                var curRange = GetRangeByPoint(ranges, decodeValue);

                char c = curRange.Item1;

                double l = curRange.Item2.Item1;
                double r = curRange.Item2.Item2;

                double delta = r - l;

                ans.Append(c);
            
                decodeValue = (decodeValue - l) / delta;
            }

            FunctionResult<string> res2 = Decode(remainingString);

            if (res2.IsOk())
                return new FunctionResult<string>(ans.ToString() + "\n" + res2.Result);
            else
                return new FunctionResult<string>(ans.ToString(), res2.Error);
        }

        protected static (char, (double, double)) GetRangeByPoint(Dictionary<char, (double, double)> ranges, double point)
        {
            foreach (var kvp  in ranges) 
            {
                if (point >= kvp.Value.Item1 && point <= kvp.Value.Item2)
                    return (kvp.Key, (kvp.Value.Item1, kvp.Value.Item2));
            }

            return ('\0', (-1, -1));
        }

        protected static (FunctionResult<Dictionary<char, (double, double)>>, int stepsCount, double decodeValue, string remainingString) ReadRangeTable(string s)
        {
            string[] lines = s.GetLines();

            Dictionary<char, (double, double)> codingDictionary = new Dictionary<char, (double, double)>();

            double prev = 0;

            int i = 0;

            for (; i <= lines.Length; i++)
            {
                if (lines[i].Length < 3)
                    return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать элемент таблицы границ, нет границы: " + lines[i]), 0, 0, null);

                if (lines[i][1] != ' ')
                    return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать элемент таблицы границ, неправильный разделитель: " + lines[i]), 0, 0, null);

                char symbol = lines[i][0];

                double cur;

                try
                {
                    cur = Double.Parse(lines[i].Substring(2));
                }
                catch
                {
                    return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать элемент таблицы границ, неправильная граница: " + lines[i]), 0, 0, null);
                }

                if (cur < prev) 
                    return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать элемент таблицы границ, неправильная граница: " + lines[i]), 0, 0, null);

                codingDictionary.Add(symbol, (prev, cur));
                prev = cur;

                if (prev == 1)
                    break;
            }

            if (prev != 1)
                return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать таблицу границ, нет завершающей границы: " + lines[i]), 0, 0, null);

            string[] parts = lines[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) 
                return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать строку для декодирования"), 0, 0, null);

            int stepsCount;

            try
            {
                stepsCount = Int32.Parse(parts[0]);
            }
            catch
            {
                return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать количество шагов при декодировании: " + parts[0]), 0, 0, null);
            }

            if (stepsCount <= 0)
                return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Неправильное количество шагов при декодировании: " + parts[0]), 0, 0, null);

            double decodeValue;

            try
            {
                decodeValue = Double.Parse(parts[1]);
            }
            catch
            {
                return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Не получилось считать число для декодирования: " + parts[1]), 0, 0, null);
            }

            if (decodeValue < 0 || decodeValue > 1)
                return (new FunctionResult<Dictionary<char, (double, double)>>(null, "Неправильное число для декодирования: " + parts[0]), 0, 0, null);

            string remainingString = ConcatStrings(lines, i + 2);

            return (new FunctionResult<Dictionary<char, (double, double)>>(codingDictionary), stepsCount, decodeValue, remainingString);
        }
    }
}
