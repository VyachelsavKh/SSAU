using CodeConvertor.Models.Coders.NumberCoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.StringCoders.UnequalCoders
{
    internal class AlphabeticCoder : UnequalCoder
    {
        public AlphabeticCoder()
        {
            _coderDescription = "Алфавитное кодирование";
        }

        protected override Dictionary<char, string> CreateCodingDictionary(string s)
        {
            Dictionary<char, int> frequencies = s.GetCharacterFrequencies();

            Dictionary<char, string> CodingDictionary = new Dictionary<char, string>(frequencies.Count);

            uint curNumCode = 0;

            foreach (var freq in frequencies)
            {
                string curStringCode;

                while (true)
                {
                    curStringCode = NumberCoder.ConvertToSystem(curNumCode);

                    if (curStringCode.IndexOf("00") == -1 || curStringCode.IndexOf("00") == curStringCode.Length - 2)
                        break;

                    curNumCode++;
                }

                curNumCode++;

                CodingDictionary.Add(freq.Key, curStringCode + "00");
            }

            return CodingDictionary;
        }

        protected override FunctionResult<string> Decode(Dictionary<char, string> codingDictionary, string s)
        {
            StringBuilder ans = new StringBuilder();

            StringBuilder prevCode = new StringBuilder();
            int zerosCount = 0;

            Dictionary<string, char> decodingDictionary = codingDictionary
                .ToLookup(x => x.Value, x => x.Key)
                .ToDictionary(x => x.Key, x => x.First());

            if (!decodingDictionary.ContainsKey("000"))
                return new FunctionResult<string>("", "В таблице кодов нет кода 000");

            foreach (var key in decodingDictionary.Keys)
            {
                if (!key.CheckOnZerosOnes())
                    return new FunctionResult<string>("", "Неизвестные символы в коде: " + key);
                if (!key.EndsWith("00"))
                    return new FunctionResult<string>("", "Код " + key + " не оканчивается на 00");
            }

            FunctionResult<string> inner()
            {
                if (zerosCount > 4)
                {
                    int minsCodesCount = zerosCount / 3;

                    if (zerosCount % 3 == 0 || zerosCount % 3 == 1)
                    {
                        minsCodesCount--;
                    }

                    int codeLength = prevCode.Length - minsCodesCount * 3;

                    string dec = prevCode.ToString().Substring(0, codeLength);

                    if (!decodingDictionary.ContainsKey(dec))
                        return new FunctionResult<string>("", "Не получилось декодировать последовательность " + dec + ": " + s);

                    ans.Append(decodingDictionary[dec]);

                    string minCodeStr = decodingDictionary["000"] + "";

                    ans.Append(minCodeStr.Repeat(minsCodesCount));
                }
                else
                {
                    string dec = prevCode.ToString();

                    if (!decodingDictionary.ContainsKey(dec))
                        return new FunctionResult<string>("", "Не получилось декодировать последовательность " + dec + ": " + s);

                    ans.Append(decodingDictionary[dec]);
                }

                return new FunctionResult<string>();
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                {
                    prevCode.Append(s[i]);
                    zerosCount++;
                }
                else
                {
                    if (zerosCount >= 2)
                    {
                        FunctionResult<string> r = inner();
                        if (!r.IsOk())
                            return r;

                        prevCode.Clear();
                        prevCode.Append("1");
                    }
                    else
                        prevCode.Append(s[i]);

                    zerosCount = 0;
                }
            }

            FunctionResult<string> res = inner();

            if (!res.IsOk())
                return res;

            return new FunctionResult<string>(ans.ToString());
        }
    }
}
