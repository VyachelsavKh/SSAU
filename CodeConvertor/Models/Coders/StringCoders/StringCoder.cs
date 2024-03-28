﻿using CodeConvertor.Models.Coders.NumberCoders.SystemCoders;
using CodeConvertor.Models.Coders.NumberCoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            if (inputCoder is SimpleString)
            {
                string[] inputLines = inputString.GetLines();

                for (int i = 0; i < inputLines.Length; i++)
                {
                    if (inputLines[i] == "")
                    {
                        outputResults.Append("\n");
                        continue;
                    }

                    CoderResult<string> outputRes = outputCoder.Encode(inputLines[i]);

                    if (!outputRes.IsOk())
                    {
                        outputErrors.Append(outputRes.Error + "\n");
                    }

                    outputResults.Append(outputRes.Result + "\n");
                }
            }
            else
            {
                inputString = inputString.Replace("\r", "");

                string[] InputLines = inputString.Split('\n');

                string[] lines = new string[InputLines.Length];

                for (int i = 0, j = 0; i < InputLines.Length; i++)
                    if (InputLines[i].Length != 0)
                        lines[j++] = InputLines[i];

                string toDecode = ConcatStrings(lines);

                CoderResult<string> inputRes = inputCoder.Decode(toDecode);

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
    }
}