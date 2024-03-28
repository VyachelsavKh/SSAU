using CodeConvertor.Models.Coders.NumberCoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvertor.Models.Coders.StringCoders.UnequalCoders
{
    internal abstract class UnequalCoder : StringCoder
    {
        protected class DecodingTree
        {
            private class Node
            {
                public string _value;

                public Node _left, _right;

                public Node(string value = null, Node left = null, Node right = null)
                {
                    _value = value;
                    _left = left;
                    _right = right;
                }
            }

            private Node _root, _cur;

            public DecodingTree()
            {
                Clear();
            }

            public void Clear()
            {
                _root = _cur = new Node();
            }

            private bool CurHasLeft()
            {
                return _cur._left != null;
            }

            private bool CurHasRight()
            {
                return _cur._right != null;
            }

            public bool CurVertexLeaf()
            {
                return !CurHasLeft() && !CurHasRight();
            }

            public bool MoveLeft()
            {
                if (!CurHasLeft())
                    return false;

                _cur = _cur._left;

                return true;
            }

            public bool AddLeft(string value = "")
            {
                if (CurHasLeft())
                    return false;

                _cur._left = new Node(value);

                return true;
            }

            public bool MoveRight()
            {
                if (!CurHasRight())
                    return false;

                _cur = _cur._right;

                return true;
            }

            public bool AddRight(string value = "")
            {
                if (CurHasRight())
                    return false;

                _cur._right = new Node(value);

                return true;
            }

            public void MoveRoot()
            {
                _cur = _root;
            }

            public void SetValue(string value)
            {
                _cur._value = value;
            }

            public string GetValue()
            {
                return _cur._value;
            }
        }

        protected abstract Dictionary<char, string> CreateCodingDictionary(string s);

        public override CoderResult<string> Encode(string s)
        {
            Dictionary<char, string> CodingDictionary = CreateCodingDictionary(s);

            StringBuilder ans = new StringBuilder(s.Length + CodingDictionary.Count * 5);

            ans.AppendLine(CodingDictionary.Count.ToString());

            foreach (var pair in CodingDictionary)
            {
                ans.AppendLine(pair.Key + " " + pair.Value);
            }

            for (int i = 0; i < s.Length; i++)
            {
                ans.Append(CodingDictionary[s[i]]);

                ans.Append(DelimiterString);
            }

            ans.Remove(ans.Length - DelimiterString.Length, DelimiterString.Length);

            return new CoderResult<string>(ans.ToString());
        }

        protected static (Dictionary<char, string>, CoderResult<string>, string) ReadCodingDictionary(string s)
        {
            s = s.Replace("\r", "");

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
                return (null, new CoderResult<string>("", "Не получилось считать количество строк в таблице кодов"), null);
            }

            Dictionary<char, string> codingDictionary = new Dictionary<char, string>(codesCount);

            for (int i = 0 + 1; i <= 0 + codesCount; i++)
            {
                if (lines[i].Length < 5 || lines[i][1] != ' ')
                    return (null, new CoderResult<string>("", "Не получилось считать таблицу кодов"), null);

                codingDictionary.Add(lines[i][0], lines[i].Substring(2));
            }

            string decodeString, remainingString;

            decodeString = lines[codesCount + 1];

            remainingString = ConcatStrings(lines, codesCount + 2);

            return (codingDictionary, new CoderResult<string>(decodeString), remainingString);
        }

        protected static DecodingTree CreateDecodingTree(Dictionary<char, string> EncodingDictionary)
        {
            DecodingTree tree = new DecodingTree();

            tree.MoveRoot();

            foreach (var pair in EncodingDictionary)
            {
                char value = pair.Key;

                string order = pair.Value;

                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] == '0')
                    {
                        if (!tree.MoveLeft())
                        {
                            tree.AddLeft();
                            tree.MoveLeft();
                        }
                    }
                    else
                    {
                        if (!tree.MoveRight())
                        {
                            tree.AddRight();
                            tree.MoveRight();
                        }
                    }
                }

                string setValue = "";

                setValue += value;

                tree.SetValue(setValue);

                tree.MoveRoot();
            }

            return tree;
        }

        public override CoderResult<string> Decode(string s)
        {
            if (s == "")
                return new CoderResult<string>(s);

            string remainingString;

            Dictionary<char, string> codingDictionary;

            CoderResult<string> decodeString;

            (codingDictionary, decodeString, remainingString) = ReadCodingDictionary(s);

            if (!decodeString.IsOk())
                return decodeString;

            string toDecode = decodeString.Result.RemoveSeparator(DelimiterString);

            if (!NumberCoder.CheckOnZerosOnes(toDecode))
            {
                return new CoderResult<string>("", "Не получилось декодировать, есть неизвестные символы: " + toDecode);
            }

            CoderResult<string> res = Decode(codingDictionary, toDecode);

            if (res.IsOk())
            {
                CoderResult<string> res2 = Decode(remainingString);

                return new CoderResult<string>(res.Result + "\n" + res2.Result, res2.Error);
            }
            else
            {
                return res;
            }
        }

        protected virtual CoderResult<string> Decode(Dictionary<char, string> codingDictionary, string s)
        {
            DecodingTree decodingTree = CreateDecodingTree(codingDictionary);

            StringBuilder ans = new StringBuilder(s.Length / 5);

            decodingTree.MoveRoot();

            s.RemoveSeparator(DelimiterString);

            bool successDecode = true;

            for (int i = 0; i < s.Length; i++)
            {
                if (decodingTree.CurVertexLeaf())
                {
                    ans.Append(decodingTree.GetValue());
                    decodingTree.MoveRoot();
                }

                if (s[i] == '0')
                {
                    if (!decodingTree.MoveLeft())
                    {
                        successDecode = false;
                        break;
                    }

                }
                else if (s[i] == '1')
                {
                    if (!decodingTree.MoveRight())
                    {
                        successDecode = false;
                        break;
                    }
                }
                else
                {
                    successDecode = false;
                    break;
                }
            }

            if (!successDecode)
                return new CoderResult<string>(ans.ToString(), "Не получилось декодировать: " + s);
            else
                return new CoderResult<string>(ans.ToString());
        }
    }
}
