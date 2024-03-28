using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.StringCoders
{
    internal class SubstitutionCoder : StringCoder
    {
        public SubstitutionCoder()
        {
            _coderDescription = "Кодирование подстановками";
        }

        public override FunctionResult<string> Encode(string s)
        {
            if (s == "")
                return new FunctionResult<string>(s);

            FunctionResult<Dictionary<char, string>> codingDictionary;

            string stringsCount;

            string encodeStrings;

            (codingDictionary, stringsCount, encodeStrings) = ReadCodingDictionary(s);

            if (!codingDictionary.IsOk())
                return new FunctionResult<string>("", codingDictionary.Error);

            int encodeStringCount;

            try 
            {
                encodeStringCount = Int32.Parse(stringsCount);
            }
            catch
            {
                return new FunctionResult<string>("", "Не получилось прочитать количество строк для кодирования");
            }

            string[] lines = encodeStrings.GetLines();

            StringBuilder ans = new StringBuilder();
            StringBuilder errors = new StringBuilder();

            Dictionary<char, string> encoding = codingDictionary.Result;

            for (int i = 0; i < encodeStringCount && i < lines.Length; i++)
            {
                StringBuilder curAns = new StringBuilder(lines[i].Length * 3);

                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (!encoding.ContainsKey(lines[i][j]))
                    {
                        errors.Append("Не получилось закодировать символ " + lines[i][j] + " из строки: "  + lines[i] + "\n");
                        break;
                    }
                    else
                    {
                        curAns.Append(encoding[lines[i][j]]);

                        curAns.Append(DelimiterString);
                    }
                }

                if (curAns.Length > DelimiterString.Length)
                    curAns.Remove(curAns.Length - DelimiterString.Length, DelimiterString.Length);

                ans.Append(curAns.ToString() + "\n");
            }

            ans.Remove(ans.Length - 1, 1);

            string remainingString = ConcatStrings(lines, encodeStringCount);

            FunctionResult<string> res = Encode(remainingString);

            return new FunctionResult<string>(ans.ToString() + "\n" + res.Result, errors.ToString() + res.Error);
        }

        public override FunctionResult<string> Decode(string s)
        {
            return new FunctionResult<string>("", "Нет декодера");
        }
    }
}
