using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrammarAnalyzer
{
    public partial class Grammar_analyzer : Form
    {
        Analyzer analyzer;

        public Grammar_analyzer()
        {
            InitializeComponent();

           analyzer = new Analyzer();
        }

        private void Check_button_Click(object sender, EventArgs e)
        {
            string inputString = Input_string.Text; 

            (string, int) errors = analyzer.CheckString(inputString);

            if (errors.Item2 >= 0)
            {
                Input_string.SelectionLength = 0;
                Input_string.SelectionStart = errors.Item2;
                Input_string.Focus();
            }

            if (errors.Item2 == -1 && analyzer.SemanticCheck)
            {
                int i;
                (Semantic_out.Text, i) = analyzer.CheckSemantic(inputString);

                if (i != -1)
                {
                    Input_string.SelectionLength = 0;
                    Input_string.SelectionStart = i;
                    Input_string.Focus();
                }

            }
            else
            {
                Semantic_out.Text = "Не проверяется, есть ошибка в грамматике";
            }

            Output_string.Text = errors.Item1;
        }

        private void Grammar_analyzer_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);

            if (data != null)
            {
                var fileName = data as string[];

                if (fileName.Length > 0)
                {
                    Grammar_error.Text = analyzer.ReadGrammar(fileName[0]);

                    Grammar_file.Text = fileName[0];
                }
            }
        }

        private void Grammar_analyzer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Semantic_button_Click(object sender, EventArgs e)
        {
            Semantic_button.Text = analyzer.SemanticCheckChange();

            if (!analyzer.SemanticCheck)
                Semantic_out.Text = "Не проверяется";

            if (analyzer.SemanticCheck)
                Semantic_out.Text = "Проверяется";
        }

        private void Input_state_string_TextChanged(object sender, EventArgs e)
        {
            analyzer.ChangeStartState(Input_state_string.Text);
        }

        private void Output_state_string_TextChanged(object sender, EventArgs e)
        {
            analyzer.ChangeEndState(Output_state_string.Text);
        }

        private void Generate_Code_Click(object sender, EventArgs e)
        {
            analyzer.GenerateAnalyzer("CodeOut.txt");
        }
    }
}
