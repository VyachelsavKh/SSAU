using CodeConvertor.Models;
using CodeConvertor.Models.Coders.NumberCoders;
using CodeConvertor.Models.Coders.NumberCoders.SystemCoders;
using CodeConvertor.Models.Coders.NumberCoders.EliasCoders;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeConvertor
{
    public partial class MainWindow : Window
    {
        string inputFileName;
        string outputFileName;

        private bool inputFileTouched;
        private bool outputFileTouched;

        private NumberCoder[] numberCoders;

        public MainWindow()
        {
            InitializeComponent();

            InitializeDefaultValues();

            inputFileTouched = outputFileTouched = false;

            InputFileBlock.Text = InputFileName.Text = "";
            OutputFileBlock.Text = OutputFileName.Text = "";

            InputTextBox.Margin = new Thickness(Left = 10, Top = 5, 10, 10);
            OutputTextBox.Margin = new Thickness(Left = 10, Top = 5, 10, 10);

            Activate();
        }

        private void InitializeDefaultValues()
        {
            ChangeInputFileName("input.txt");
            ChangeOutputFileName("output.txt");

            numberCoders = new NumberCoder[]
            {
                new DecimalCoder(),
                new GammaCoder(),
                new DeltaCoder(),
                new OmegaCoder(),
                new HexCoder(),
                new OctCoder(),
            };

            InputCoder.ItemsSource = numberCoders;

            InputCoder.SelectedIndex = 0;

            OutputCoder.ItemsSource = numberCoders;

            OutputCoder.SelectedIndex = 1;
        }

        private void ChangeInputFileName(string path)
        {

            inputFileName = path;
            InputFileName.Text = FileWorker.GetFilenameFromPath(path);
        }

        private void ChangeOutputFileName(string path)
        {
            outputFileName = path;
            OutputFileName.Text = FileWorker.GetFilenameFromPath(path);
        }

        private void ConvertInputToOutput()
        {
            NumberCoder left = (NumberCoder)InputCoder.SelectedItem;
            NumberCoder right = (NumberCoder)OutputCoder.SelectedItem;

            if (left != null && right != null)
            {

                left.DelimiterString = DelimiterString.Text;
                right.DelimiterString = DelimiterString.Text;

                OutputTextBox.Text = NumberCoder.ConvertString(InputTextBox.Text, left, right);
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConvertInputToOutput();
        }

        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            InputTextBox.Focus();
        }

        private void ChangeTextBoxesMargin()
        {
            InputTextBox.Margin = new Thickness(Left = 10, Top = 25, 10, 10);
            OutputTextBox.Margin = new Thickness(Left = 10, Top = 25, 10, 10);
        }

        private void OpenInput_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            string result = FileWorker.OpenFileDialog(out fileName);

            if (fileName != null)
            {
                if (!inputFileTouched)
                {
                    InputFileBlock.Text = "Входной файл: ";
                    ChangeTextBoxesMargin();
                    inputFileTouched = true;
                }

                ChangeInputFileName(fileName);

                InputTextBox.Clear();
                InputTextBox.Text = result;
                InputTextBox.Focus();
                InputTextBox.Select(InputTextBox.Text.Length, 0);
            }
        }

        private void SaveAsInput_Click(object sender, RoutedEventArgs e)
        {
            string filename = FileWorker.SaveFileDialog(InputTextBox.Text, InputFileName.Text);

            if (filename != null)
            {
                if (!inputFileTouched)
                {
                    inputFileTouched = true;
                    ChangeTextBoxesMargin();
                    InputFileBlock.Text = "Входной файл: ";
                }

                ChangeInputFileName(filename);
            }
        }

        private void SaveInput_Click(object sender, RoutedEventArgs e)
        {
            if (!inputFileTouched)
            {
                string filename = FileWorker.SaveFileDialog(InputTextBox.Text, InputFileName.Text);

                if (filename != null)
                {
                    inputFileTouched = true;
                    ChangeTextBoxesMargin();
                    InputFileBlock.Text = "Входной файл:";

                    ChangeInputFileName(filename);
                }
            }
            else
                FileWorker.WriteToFile(inputFileName, InputTextBox.Text);
        }

        private void SaveAsOutput_Click(object sender, RoutedEventArgs e)
        {
            string filename = FileWorker.SaveFileDialog(OutputTextBox.Text, OutputFileName.Text);

            if (filename != null)
            {
                if (!outputFileTouched)
                {
                    outputFileTouched = true;
                    ChangeTextBoxesMargin();
                    OutputFileBlock.Text = "Входной файл:";
                }

                ChangeOutputFileName(filename);
            }
        }

        private void SaveOutput_Click(object sender, RoutedEventArgs e)
        {
            if (!outputFileTouched)
            {
                string filename = FileWorker.SaveFileDialog(OutputTextBox.Text, OutputFileName.Text);

                if (filename != null)
                {
                    outputFileTouched = true;
                    ChangeTextBoxesMargin();
                    OutputFileBlock.Text = "Выходной файл:";

                    ChangeOutputFileName(filename);
                }
            }
            else
                FileWorker.WriteToFile(outputFileName, OutputTextBox.Text);
        }

        private void DelimiterString_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConvertInputToOutput();
        }

        private void CoderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertInputToOutput();
        }

        private string GetData(string str)
        {
            string ans = str.Replace("Не получилось закодировать: ", "");

            ans = ans.Replace("Не получилось декодировать: ", "");

            return ans;
        }

        private void ChangeCodes_Click(object sender, RoutedEventArgs e)
        {
            string newInput = GetData(OutputTextBox.Text);

            InputTextBox.Clear();

            int t = OutputCoder.SelectedIndex;
            OutputCoder.SelectedIndex = InputCoder.SelectedIndex;
            InputCoder.SelectedIndex = t;

            InputTextBox.Text = newInput;
            InputTextBox.Focus();
            InputTextBox.Select(InputTextBox.Text.Length, 0);

            ConvertInputToOutput();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.S)
            {
                SaveAsOutput_Click(null, null);
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                SaveOutput_Click(null, null);
            }
        }

        private void CopyOutput_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(GetData(OutputTextBox.Text));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double maxHeight = Math.Max(InputTopPanel.ActualHeight, OutputTopPanel.ActualHeight);

            MenuRow.Height = new GridLength(maxHeight + 10);

            /*
            Thickness newMargin = new Thickness();

            bool deleteInputRow = false;
            bool deleteOutputtRow = false;

            if (InputRow.ActualWidth <= InputCoder.Margin.Left + InputCoder.Width 
                + 5 + ClearInput.ActualWidth + 10)
            {
                newMargin.Top = InputMenu.ActualHeight + InputMenu.Margin.Top + 5;
                newMargin.Left = InputMenu.Margin.Left;
                ClearInput.Margin = newMargin;

                newMargin.Left += ClearInput.ActualWidth + 5;
                DelimiterBorder.Margin = newMargin;

                deleteInputRow = false;
            }
            else if (InputRow.ActualWidth <= InputCoder.Margin.Left + InputCoder.Width 
                + 5 + ClearInput.ActualWidth 
                + 5 + DelimiterBorder.ActualWidth + 10)
            {
                newMargin.Top = InputMenu.Margin.Top;
                newMargin.Left = InputCoder.Width + InputCoder.Margin.Left + 5;
                ClearInput.Margin = newMargin;

                newMargin.Top += InputMenu.ActualHeight;
                newMargin.Left = InputMenu.Margin.Left;
                DelimiterBorder.Margin = newMargin;

                deleteInputRow = false;
            }
            else
            {
                newMargin.Top = InputMenu.Margin.Top;
                newMargin.Left = InputCoder.Width + InputCoder.Margin.Left + 5;
                ClearInput.Margin = newMargin;

                newMargin.Top = InputCoder.Margin.Top;
                newMargin.Left = ClearInput.ActualWidth + ClearInput.Margin.Left + 5;
                DelimiterBorder.Margin = newMargin;

                deleteInputRow = true;
            }

            if (OutputRow.ActualWidth <= OutputCoder.Margin.Left + OutputCoder.Width
                + 5 + ChangeCodes.ActualWidth + 10)
            {
                newMargin.Top = OutputMenu.ActualHeight + OutputMenu.Margin.Top + 5;
                newMargin.Left = OutputMenu.Margin.Left;
                ChangeCodes.Margin = newMargin;

                newMargin.Left += ChangeCodes.ActualWidth + 5;
                CopyOutput.Margin = newMargin;

                deleteOutputtRow = false;
            }
            else if (OutputRow.ActualWidth <= OutputCoder.Margin.Left + OutputCoder.Width
                + 5 + ChangeCodes.ActualWidth
                + 5 + CopyOutput.ActualWidth + 10)
            {
                newMargin.Top = OutputCoder.Margin.Top;
                newMargin.Left = OutputCoder.Margin.Left + OutputCoder.Width + 5;
                ChangeCodes.Margin = newMargin;

                newMargin.Top += OutputMenu.ActualHeight + 5 + 5;
                newMargin.Left = OutputMenu.Margin.Left;
                CopyOutput.Margin = newMargin;

                deleteOutputtRow = false;
            }
            else
            {
                newMargin.Top = OutputCoder.Margin.Top;
                newMargin.Left = OutputCoder.Margin.Left + OutputCoder.Width + 5;
                ChangeCodes.Margin = newMargin;

                newMargin.Top = OutputCoder.Margin.Top;
                newMargin.Left = ChangeCodes.Margin.Left + ChangeCodes.ActualWidth + 5;
                CopyOutput.Margin = newMargin;

                deleteOutputtRow = true;
            }
            
            if (deleteInputRow && deleteOutputtRow)
                MenuRow.Height = new GridLength(50);
            else
                MenuRow.Height = new GridLength(85);
            */
        }
    }
}
