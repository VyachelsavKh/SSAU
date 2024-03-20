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

        public MainWindow()
        {
            InitializeComponent();

            InitializeDefaultValues();
        }

        private void InitializeDefaultValues()
        {
            ChangeInputFileName("input.txt");
            ChangeOutputFileName("output.txt");

            InputCoder.ItemsSource = new NumberCoder[]
            {
                new DecimalCoder(),
                new GammaCoder(),
                new DeltaCoder(),
                new OmegaCoder()
            };

            InputCoder.SelectedIndex = 0;

            OutputCoder.ItemsSource = new NumberCoder[]
            {
                new DecimalCoder(),
                new GammaCoder(),
                new DeltaCoder(),
                new OmegaCoder()
            };

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

        private void OpenInput_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            string result = FileWorker.OpenFileDialog(out fileName);

            if (fileName != null)
            {
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
            ChangeInputFileName(filename);
        }

        private void SaveAsOutput_Click(object sender, RoutedEventArgs e)
        {
            string filename = FileWorker.SaveFileDialog(OutputTextBox.Text, OutputFileName.Text);
            ChangeOutputFileName(filename);
        }

        private void SaveInput_Click(object sender, RoutedEventArgs e)
        {
            FileWorker.WriteToFile(inputFileName, InputTextBox.Text);
        }

        private void SaveOutput_Click(object sender, RoutedEventArgs e)
        {
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
                string filename = FileWorker.SaveFileDialog(OutputTextBox.Text, OutputFileName.Text);
                ChangeOutputFileName(filename);
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                FileWorker.WriteToFile(outputFileName, OutputTextBox.Text);
            }
        }
    }
}
