using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms;

namespace GrammarAnalyzer
{
    internal class Analyzer
    {
        private string GrammarFileName;
        private bool GrammarExist;
        private bool semanticCheck;

        private string StartState;
        private string EndState;

        private Dictionary<string, Dictionary<char, string>> GrammarGraph;

        public Analyzer() 
        {
            GrammarExist = false;
            semanticCheck = true;

            StartState = "S";
            EndState = "F";
        }

        public string SemanticCheckChange()
        {
            semanticCheck = !semanticCheck;

            if (semanticCheck) return "Вкл";
            else return "Выкл";
        }

        public bool SemanticCheck
        {
            get
            {
                return semanticCheck;
            }
        }

        public void ChangeStartState(string start)
        { 
            StartState = start;
        }

        public void ChangeEndState(string end)
        {
            EndState = end;
        }

        private (string, List<char>, string) ReadLine(string line)
        {
            bool readStart = true;

            string start = "";
            string end = "";
            List<char> move = new List<char>();

            for (int j = 0; j < line.Length; j++)
            {
                char c = line[j];

                if (readStart)
                {
                    if (IsLetter(c) || IsNum(c)) start += c;
                    else if (c == '-')
                    {
                        readStart = false;
                        j++;
                    }
                    else continue;
                }
                else
                {
                    if (IsLetter(c) || IsNum(c)) end += c;
                    else if (c == '[')
                    {
                        break;
                    }
                    else continue;
                }
            }

            int labelBegin = line.IndexOf("label", 0, line.Length - 1);

            if (labelBegin != -1)
            {
                for (; line[labelBegin] != '"'; labelBegin++) ;
                labelBegin++;
                for (; line[labelBegin] != '"'; labelBegin++)
                {
                    if (line[labelBegin] == '|') continue;
                    else if (line[labelBegin] == '!')
                    {
                        char notUse = line[labelBegin + 1];

                        move.Remove(notUse);

                        labelBegin++;
                    }
                    else
                    {
                        move.Add(line[labelBegin]);
                    }
                }
            }

            return (start, move, end);
        }

        public string ReadGrammar(string grammarFileName)
        {
            GrammarGraph = new Dictionary<string, Dictionary<char, string>>();

            GrammarFileName = grammarFileName;
            GrammarExist = true;

            string[] lines = File.ReadAllLines(GrammarFileName);

            string start;
            string end;
            List<char> move;

            bool error = false;
            string Error = "";

            foreach (string line in lines) 
            {
                if (line.Length <= 2) continue;
                if (!line.Contains("->")) continue;

                (start, move, end) = ReadLine(line);

                foreach (char m in move)
                {
                    if (GrammarGraph.ContainsKey(start))
                    {
                        var End = GrammarGraph[start];

                        if (End.ContainsKey(m))
                        {
                            if (End[m] != "")
                            {
                                string m_str = "" + m;
                                if (m == ' ') m_str = "PROBEL";

                                error = true;
                                Error += "Была команда: " +
                                        start + " -> " + End[m] + " по символу " + m_str + "\n" +
                                        "Встретилась команда " +
                                        start + " -> " + end + " по символу " + m_str + "\n";
                            }
                        }
                        else
                        {
                            GrammarGraph[start].Add(m, end);
                        }
                    }
                    else
                    {
                        GrammarGraph.Add(start, new Dictionary<char, string>());
                        GrammarGraph[start].Add(m, end);
                    }
                }
            }

            if (error) return Error;

            return "Нет ошибок в грамматике";
        }

        public (string, int) CheckString(string inputString)
        {
            if (!GrammarExist) return ("Нет грамматики", -2);

            string state = StartState;

            string out_str;
            string out_add;
            int count;

            int i = 0;

            for (; i < inputString.Length; i++)
            {
                if (GrammarGraph.ContainsKey(state))
                {
                    var End = GrammarGraph[state];

                    if (GrammarGraph[state].ContainsKey(inputString[i]))
                    {
                        state = End[inputString[i]];
                    }
                    else
                    {
                        out_add = "Нет перехода из состояния " + state +
                            " по символу ";
                        if (inputString[i] != ' ') out_add += inputString[i];
                        else out_add += "PROBEL";

                        if (i != 0)
                        {
                            if (inputString[i - 1] != ' ') out_str = "После символа " + inputString[i - 1] + " ожидаются символы: \n";
                            else out_str = "После символа PROBEL ожидаются символы: \n";
                        }
                        else out_str = "В начале строки ожидаются символы: \n";

                        count = 0;
                        foreach (var expect_state in GrammarGraph[state])
                        {
                            if (count >= GrammarGraph[state].Count() / 3 && count >= 20)
                            {
                                out_str += "\n";
                                count = 0;
                            }

                            if (expect_state.Key != ' ') out_str += expect_state.Key;
                            else out_str += "PROBEL";

                            if (expect_state.Key != GrammarGraph[state].Last().Key)
                            out_str += ", ";

                            count++;
                        }
                        out_str += "\n";

                        return (out_str + out_add, i);
                    }
                }
                else if (state != EndState) return ("Нет переходов из состояния " + state, i);

                if (state == EndState) break;
            }
            
            if (state != EndState)
            {
                out_str = "";
                count = 0;
                foreach (var expect_state in GrammarGraph[state])
                {
                    if (count >= GrammarGraph[state].Count() / 3 && count >= 20)
                    {
                        out_str += "\n";
                        count = 0;
                    }

                    if (expect_state.Key != ' ') out_str += expect_state.Key;
                    else out_str += "PROBEL";

                    if (expect_state.Key != GrammarGraph[state].Last().Key)
                        out_str += ", ";

                    count++;
                }
                out_str += "\n";

                if (inputString.Length == 0) return ("В начале строки ожидаются символы: \n" + out_str, 0);
                else if (inputString[inputString.Length - 1] != ' ') return ("После символа " + inputString[inputString.Length - 1] + " ожидаются символы: \n" + out_str, inputString.Length);
                else return ("После символа PROBEL ожидаются символы: \n" + out_str, inputString.Length);
            }

            i++;

            i = SkipSpace(inputString, i);

            if (i != inputString.Length)
            {
                return ("После ; строка не рассматривается", i);
            }

            return ("Нет ошибок", -1);
        }

        public int SkipSpace(string inputString, int i)
        {
            for (; i < inputString.Length && inputString[i] == ' '; i++) ;

            return i;
        }

        private bool IsLetter(char ch)
        {
            int c = ch;
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '_';
        }

        private bool IsNum(char ch) 
        {
            return ch >= '0' && ch <= '9';
        }

        private (string, int) ReadStr(string inputString, int i)
        {
            string readStr = "";

            i = SkipSpace(inputString, i);

            for (; inputString[i] != ' ' && i < inputString.Length &&
                (IsLetter(inputString[i]) || IsNum(inputString[i])); i++) 
                readStr += inputString[i];

            return (readStr, i);
        }

        private bool IsKeyWord(string str)
        {
            return str == "procedure" || str == "function" ||
                str == "var" || str == "byte" ||
                str == "integer" || str == "real" ||
                str == "char" || str == "string" ||
                str == "boolean";
        }

        public (string, int) CheckSemantic(string inputString)
        {
            string inputStringLower = inputString.ToLower();

            string Semantic = "";
            string ErrorArgument = "";

            bool ProcFunc = false;

            string FPName = "";
            string FunctionType = "";

            int i = SkipSpace(inputString, 0);

            if (inputStringLower[i] == 'f') ProcFunc = true;

            (_, i) = ReadStr(inputString, i);

            (FPName, _) = ReadStr(inputString, i);

            if (!ProcFunc)                
                Semantic += "Процедура " + FPName + "\n";

            int LBracketId = inputString.IndexOf('(');

            if (ProcFunc)
            {
                if (LBracketId != -1)
                {
                    i = inputString.IndexOf(')');

                    for (; !IsLetter(inputString[i]); i++) ;
                }
                else
                    i = inputString.IndexOf(':') + 1;

                (FunctionType, i) = ReadStr(inputString, i);

                Semantic += "Функция " + FPName + " возвращает значение типа " + FunctionType + "\n";
            }

            List<string> idNamePrev = new List<string>();
            List<string> idName = new List<string>();
            Dictionary<string, List<string>> idTypeName = new Dictionary<string, List<string>>();

            string idname = "", idtype = "";
            string idnameLower;

            int var_count = 0;

            if (LBracketId != -1)
            {
                i = LBracketId + 1;

                while (inputString[i] != ')')
                {
                    (idname, i) = ReadStr(inputString, i);

                    idnameLower = idname.ToLower();

                    if (IsKeyWord(idnameLower) && var_count >= 1)
                    {
                        ErrorArgument += "Использование ключевого слова " + idnameLower + " в качестве аргумента\n";

                        return (ErrorArgument, i - idname.Length);
                    }

                    if (idnameLower == "var")
                    {
                        var_count++;
                        continue;
                    }

                    if (idname.Length > 8)
                    {
                        ErrorArgument += "Имя идентификатора " + idname + " больше 8 символов";
                        return (ErrorArgument, i - idname.Length);
                    }

                    if (idNamePrev.Contains(idname))
                    {
                        ErrorArgument += "Повторное использование идентификатора " + idname + "\n";

                        return (ErrorArgument, i - idname.Length);
                    }

                    idName.Add(idname);
                    idNamePrev.Add(idname);

                    i = SkipSpace(inputString, i);

                    if (inputString[i] == ',')
                    {
                        i++;
                        continue;
                    }

                    if (inputString[i] == ':')
                    {
                        i++;

                        (idtype, i) = ReadStr(inputString, i);

                        idtype = idtype.ToLower();

                        if (!idTypeName.ContainsKey(idtype))
                            idTypeName.Add(idtype, new List<string>());

                        foreach (string id in idName)
                        {
                            idTypeName[idtype].Add(id);
                        }

                        var_count = 0;

                        idName.Clear();
                    }

                    if (inputString[i] == ';') i++;
                }
            }

            Semantic += "Аргументы: \n";

            foreach (var id in idTypeName)
            {
                Semantic += id.Key;

                if (id.Key == "byte")
                    Semantic += "(1)";

                if (id.Key == "integer")
                    Semantic += "(4)";

                if (id.Key == "real")
                    Semantic += "(8)";

                if (id.Key == "char")
                    Semantic += "(1)";

                if (id.Key == "boolean")
                    Semantic += "(1)";

                if (id.Key == "string")
                {
                    Semantic += "(16)";
                }

                Semantic += ": ";

                foreach (string idnamecur in id.Value)
                { 
                    Semantic += idnamecur + ", ";
                }

                Semantic = Semantic.Remove(Semantic.Length - 2);

                Semantic += "\n";
            }

            i = inputStringLower.IndexOf("begin");

            ReadStr(inputString, i);

            return (Semantic, -1);
        }

        public int GenerateAnalyzer(string filePath)
        {
            FileStream outFile = File.Open(filePath, FileMode.Create);
            
            if (!GrammarExist) return -2;

            const string str1 = "\ncase EnumState.";
            const string str2 = ":\n{";
            const string str3 = "\n\tif (";
            const string str4 = ")\n\t{\n\t\tState = EnumState.";
            const string str5 = "\n\tbreak;\n}";
            const string str6 = "\n\telse if (";
            const string str7 = "\n\telse\n\t{\n\t\tSetError(Err.AnotherLetterExpected, _Pos);\n\t}";

            string curSwitch = "";
            string Enum = "enum EnumState {";

            foreach (var a in GrammarGraph)
            {
                Enum += a.Key + ", ";

                curSwitch += str1 + a.Key + str2;

                List<List<char>> symbols = new List<List<char>>();
                List<string> states = new List<string>();

                foreach (var b in a.Value)
                {
                    if (!states.Contains(b.Value))
                    {
                        states.Add(b.Value);
                        symbols.Add(new List<char>());
                    }

                    symbols[states.IndexOf(b.Value)].Add(b.Key);
                }

                curSwitch += str3;

                for (int j = 0; j < symbols[0].Count - 1; j++)
                {
                    curSwitch += "c == '" + symbols[0][j] + "' || ";
                }

                curSwitch += "c == '" + symbols[0].Last() + "'";

                curSwitch += str4 + states[0] + "\n\t}";

                for (int i = 1; i < states.Count; i++)
                {
                    curSwitch += str6;

                    for (int j = 0; j < symbols[i].Count - 1; j++)
                    {
                        curSwitch += "c == '" + symbols[i][j] + "' || ";
                    }

                    curSwitch += "c == '" + symbols[i].Last() + "'";

                    curSwitch += str4 + states[i] + ";\n\t}";
                }

                curSwitch += str7;

                curSwitch += str5;
            }

            Enum += "}\n";

            string outStr = Enum + curSwitch;

            outFile.Write(Encoding.ASCII.GetBytes(outStr), 0, outStr.Length);

            outFile.Flush();
            outFile.Close();

            return 0;
        }
    }
}