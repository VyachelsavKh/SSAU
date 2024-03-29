using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.StringCoders.UnequalCoders
{
    internal class ShannonCoder : UnequalCoder
    {
        public ShannonCoder()
        {
            _coderDescription = "Код Шеннона-Фано";
        }

        protected override Dictionary<char, string> CreateCodingDictionary(string s)
        {
            Dictionary<char, int> freqs = s.GetCharacterFrequencies();

            KeyValuePair<char, int>[] frequencies = freqs.ToArray();

            int sum = 0;

            Dictionary<char, string> CodingDictionary = new Dictionary<char, string>(frequencies.Length);

            foreach (var f in frequencies)
            {
                sum += f.Value;
                CodingDictionary.Add(f.Key, "");
            }

            Divide(frequencies, sum, 0, frequencies.Length - 1, CodingDictionary);

            return CodingDictionary;
        }

        protected void Divide(KeyValuePair<char, int>[] frequencies, int sum, int l, int r, Dictionary<char, string> CodingDictionary)
        {
            if (r == l)
            {
                return;
            }

            if (r - l == 1)
            {
                char l_sym = frequencies[l].Key;
                char r_sym = frequencies[r].Key;

                string code_l = CodingDictionary[l_sym];
                string code_r = CodingDictionary[r_sym];

                CodingDictionary[l_sym] = code_l + "0";
                CodingDictionary[r_sym] = code_r + "1";

                return;
            }

            int left_sum = 0;
            int right_sum = sum;

            int prev_dif = Math.Abs(left_sum - right_sum);
            int new_dif = 0;

            int divide_pos = l;

            for (int i = l; i <= r; i++)
            {
                left_sum += frequencies[i].Value;
                right_sum -= frequencies[i].Value;

                new_dif = Math.Abs(left_sum - right_sum);

                if (new_dif < prev_dif)
                {
                    prev_dif = new_dif;
                }
                else
                {
                    divide_pos = i;

                    left_sum -= frequencies[i].Value;
                    right_sum += frequencies[i].Value;

                    break;
                }
            }

            for (int i = l; i < divide_pos; i++)
            {
                char cur_sym = frequencies[i].Key;

                string cur_code = CodingDictionary[cur_sym];

                CodingDictionary[cur_sym] = cur_code + "0";
            }

            for (int i = divide_pos; i <= r; i++)
            {
                char cur_sym = frequencies[i].Key;

                string cur_code = CodingDictionary[cur_sym];

                CodingDictionary[cur_sym] = cur_code + "1";
            }

            Divide(frequencies, left_sum, l, divide_pos - 1, CodingDictionary);
            Divide(frequencies, right_sum, divide_pos, r, CodingDictionary);
        }
    }
}
