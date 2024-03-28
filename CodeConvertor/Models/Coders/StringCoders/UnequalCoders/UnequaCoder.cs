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

        public override FunctionResult<string> Encode(string s)
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

            return new FunctionResult<string>(ans.ToString());
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

        public override FunctionResult<string> Decode(string s)
        {
            if (s == "")
                return new FunctionResult<string>(s);

            string remainingString;

            FunctionResult<Dictionary<char, string>> codingDictionary;

            string decodeString;

            (codingDictionary, decodeString, remainingString) = ReadCodingDictionary(s);

            if (!codingDictionary.IsOk())
                return new FunctionResult<string>("", codingDictionary.Error);

            string toDecode = decodeString.RemoveSeparator(DelimiterString);

            if (!toDecode.CheckOnZerosOnes())
            {
                return new FunctionResult<string>("", "Не получилось декодировать, есть неизвестные символы: " + toDecode);
            }

            FunctionResult<string> res = Decode(codingDictionary.Result, toDecode);

            if (res.IsOk())
            {
                FunctionResult<string> res2 = Decode(remainingString);

                return new FunctionResult<string>(res.Result + "\n" + res2.Result, res2.Error);
            }
            else
            {
                return res;
            }
        }

        protected virtual FunctionResult<string> Decode(Dictionary<char, string> codingDictionary, string s)
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
                return new FunctionResult<string>(ans.ToString(), "Не получилось декодировать: " + s);
            else
                return new FunctionResult<string>(ans.ToString());
        }
    }
}
