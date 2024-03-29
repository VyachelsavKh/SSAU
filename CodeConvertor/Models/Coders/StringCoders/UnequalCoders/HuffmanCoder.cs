using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeConvertor.Models.Coders.StringCoders.UnequalCoders
{
    internal class HuffmanCoder : UnequalCoder
    {
        public HuffmanCoder()
        {
            _coderDescription = "Код Хаффмана";
        }

        protected class PriorityQueue<T> where T : IComparable<T>
        {
            private List<T> items = new List<T>();

            public int Count => items.Count;

            public void Enqueue(T item)
            {
                items.Add(item);
                int i = items.Count - 1;
                while (i > 0)
                {
                    int parent = (i - 1) / 2;
                    if (items[i].CompareTo(items[parent]) >= 0)
                        break;
                    Swap(i, parent);
                    i = parent;
                }
            }

            public T Dequeue()
            {
                if (items.Count == 0)
                    throw new InvalidOperationException("Priority queue is empty");

                T frontItem = items[0];
                int lastIndex = items.Count - 1;
                items[0] = items[lastIndex];
                items.RemoveAt(lastIndex);

                int currentIndex = 0;
                while (true)
                {
                    int leftChildIndex = currentIndex * 2 + 1;
                    int rightChildIndex = currentIndex * 2 + 2;
                    int smallestChildIndex = currentIndex;

                    if (leftChildIndex < items.Count && items[leftChildIndex].CompareTo(items[smallestChildIndex]) < 0)
                        smallestChildIndex = leftChildIndex;

                    if (rightChildIndex < items.Count && items[rightChildIndex].CompareTo(items[smallestChildIndex]) < 0)
                        smallestChildIndex = rightChildIndex;

                    if (smallestChildIndex == currentIndex)
                        break;

                    Swap(currentIndex, smallestChildIndex);
                    currentIndex = smallestChildIndex;
                }

                return frontItem;
            }

            public T Peek()
            {
                if (items.Count == 0)
                    throw new InvalidOperationException("Priority queue is empty");

                return items[0];
            }

            private void Swap(int i, int j)
            {
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }

        protected class HuffmanNode : IComparable<HuffmanNode>
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }

            public HuffmanNode(char symbol, int frequency, HuffmanNode left = null, HuffmanNode right = null)
            {
                Symbol = symbol;
                Frequency = frequency;
                Left = left;
                Right = right;
            }

            public int CompareTo(HuffmanNode other)
            {
                return Frequency - other.Frequency;
            }
        }

        protected override Dictionary<char, string> CreateCodingDictionary(string s)
        {
            Dictionary<char, int> frequencies = s.GetCharacterFrequencies();

            PriorityQueue<HuffmanNode> pq = new PriorityQueue<HuffmanNode>();

            foreach (var kvp in frequencies)
            {
                HuffmanNode cur = new HuffmanNode(kvp.Key, kvp.Value);

                pq.Enqueue(cur);
            }

            while (pq.Count > 1)
            {
                HuffmanNode left = pq.Dequeue();
                HuffmanNode right = pq.Dequeue();

                HuffmanNode midNode = new HuffmanNode('\0', left.Frequency + right.Frequency, left, right);

                pq.Enqueue(midNode);
            }

            Dictionary<char, string> codingDictionary = new Dictionary<char, string>();

            Transform(codingDictionary, "", pq.Peek());

            var sortedDictionary = codingDictionary.OrderBy(x => x.Value.Length)
                                         .ThenBy(x => x.Value)
                                         .ToDictionary(x => x.Key, x => x.Value);

            return sortedDictionary;
        }

        protected static void Transform(Dictionary<char, string> codingDictionary, string curCode, HuffmanNode node)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
            {
                codingDictionary[node.Symbol] = curCode;
                return;
            }

            Transform(codingDictionary, curCode + "0", node.Left);
            Transform(codingDictionary, curCode + "1", node.Right);
        }
    }
}
