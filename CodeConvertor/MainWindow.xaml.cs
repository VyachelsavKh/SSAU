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
using CodeConvertor.Models.Coders;
using CodeConvertor.Models.Coders.StringCoders;
using CodeConvertor.Models.Coders.StringCoders.UnequalCoders;
using CodeConvertor.Models.Coders.StringCoders.CompressionCoders;

namespace CodeConvertor
{
    public partial class MainWindow : Window
    {
        string inputFileName;
        string outputFileName;

        private bool inputFileTouched;
        private bool outputFileTouched;

        private NumberCoder[] numberCoders;
        private StringCoder[] stringCoders;

        private Coder[] inputCoders;

        private enum CoderType { Null, NumberCoder, StringCoder};
        private CoderType InputCoderType;

        public MainWindow()
        {
            InitializeComponent();

            InitializeDefaultValues();

            inputFileTouched = outputFileTouched = false;

            InputFileBlock.Text = InputFileName.Text = "";
            OutputFileBlock.Text = OutputFileName.Text = "";

            InputTextBox.Margin = new Thickness(Left = 10, Top = 5, 10, 10);
            OutputTextBox.Margin = new Thickness(Left = 10, Top = 5, 10, 10);

            CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            Activate();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void InitializeDefaultValues()
        {
            ChangeInputFileName("input.txt");
            ChangeOutputFileName("output.txt");

            stringCoders = new StringCoder[]
            {
                new SimpleString(),
                new AlphabeticCoder(),
                new ShannonCoder(),
                new HuffmanCoder(),
                new SubstitutionCoder(),
                new ArithmeticCoder(),
            };

            numberCoders = new NumberCoder[]
            {
                new DecimalCoder(),
                new BinCoder(),
                new HexCoder(),
                new OctCoder(),
                new GammaCoder(),
                new DeltaCoder(),
                new OmegaCoder(),
                new HammingCoder(),
            };

            inputCoders = new Coder[0];
            inputCoders = inputCoders.Concat(numberCoders).ToArray();
            inputCoders = inputCoders.Concat(stringCoders).ToArray();

            InputCoder.ItemsSource = inputCoders;

            InputCoderType = CoderType.Null;
            InputCoder.SelectedIndex = 0;
        }

        private void ConvertInputToOutput()
        {
            Coder left = (Coder)InputCoder.SelectedItem;
            Coder right = (Coder)OutputCoder.SelectedItem;

            if (left != null && right != null)
            {
                left.DelimiterString = DelimiterString.Text;
                right.DelimiterString = DelimiterString.Text;

                if (left is NumberCoder)
                {
                    NumberCoder l = left as NumberCoder;
                    NumberCoder r = right as NumberCoder;

                    (string inputErrors, string outputResult, string outputErrors) = NumberCoder.TranslateString(InputTextBox.Text, l, r);

                    InputCoderErrors.Text = inputErrors;
                    OutputTextBox.Text = outputResult;
                    OutputCoderErrors.Text = outputErrors;
                }
                else if (left is StringCoder)
                {
                    StringCoder l = left as StringCoder;
                    StringCoder r = right as StringCoder;

                    (string inputErrors, string outputResult, string outputErrors) = StringCoder.TranslateString(InputTextBox.Text, l, r);

                    InputCoderErrors.Text = inputErrors;
                    OutputTextBox.Text = outputResult;
                    OutputCoderErrors.Text = outputErrors;
                }
            }
        }

        private void InputCoderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputCoder.SelectedItem is NumberCoder && InputCoderType != CoderType.NumberCoder)
            {
                InputCoderType = CoderType.NumberCoder;

                OutputCoder.ItemsSource = numberCoders;

                OutputCoder.SelectedIndex = 1;
            }
            else if (InputCoder.SelectedItem is StringCoder && InputCoderType != CoderType.StringCoder)
            {
                InputCoderType = CoderType.StringCoder;

                OutputCoder.ItemsSource = stringCoders;

                OutputCoder.SelectedIndex = 1;
            }

            ConvertInputToOutput();
        }

        private void OutputCoderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertInputToOutput();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConvertInputToOutput();
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

        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            InputTextBox.Focus();
        }

        private void ChangeTextBoxesMargin()
        {
            InputTextBox.Margin = new Thickness(Left = 10, Top = 25, 10, 10);
            OutputTextBox.Margin = new Thickness(Left = 10, Top = 25, 10, 10);

            CenterWindowOnScreen();
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

        private void ChangeCodes_Click(object sender, RoutedEventArgs e)
        {
            string newInput = OutputTextBox.Text;

            InputTextBox.Clear();

            Coder inputCoder = (Coder)InputCoder.SelectedItem;
            Coder outputCoder = (Coder)OutputCoder.SelectedItem;

            int i = 0;
            foreach (var coder in InputCoder.ItemsSource)
            {
                if (coder == outputCoder)
                { 
                    InputCoder.SelectedIndex = i;
                    break;
                }
                i++;
            }

            i = 0;
            foreach (var coder in OutputCoder.ItemsSource)
            {
                if (coder == inputCoder)
                {
                    OutputCoder.SelectedIndex = i;
                    break;
                }
                i++;
            }

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
            Clipboard.SetText(OutputTextBox.Text);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double maxHeight = 0;

            if (InputTopPanel.ActualWidth >= 485)
            {
                InputCoder.Width = 175 + InputTopPanel.ActualWidth - 485;
                maxHeight = 45;
            }
            else
            {
                InputCoder.Width = 175;
                maxHeight = 80;
            }

            if (OutputTopPanel.ActualWidth >= 470)
            {
                OutputCoder.Width = 175 + OutputTopPanel.ActualWidth - 470;
            }
            else
            {
                OutputCoder.Width = 175;
            }

            MenuRow.Height = new GridLength(maxHeight);
        }
    }
}
