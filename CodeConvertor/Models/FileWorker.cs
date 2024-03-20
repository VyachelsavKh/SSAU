using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeConvertor.Models
{
    internal class FileWorker
    {
        public static string GetFilenameFromPath(string path)
        {
            int lastSlashIndex = path.LastIndexOf('\\');

            if (lastSlashIndex == -1)
            {
                return path;
            }

            string filename = path.Substring(lastSlashIndex + 1);

            return filename;
        }

        public static void WriteToFile(string path, string contents)
        {
            try
            {
                File.WriteAllText(path, contents);
            }
            catch 
            {
                MessageBox.Show("Не получилось записать в файл");
            }
        }

        public static string ReadFromFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch
            {
                MessageBox.Show("Не получилось прочитать файл");
            }
            return "";
        }

        public static string SaveFileDialog(string str, string defaulFileName = "result.txt")
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = defaulFileName;
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Обычный текстовый файл (*.txt)|*.txt|All files (*.*)|*.*";

            bool? result = dialog.ShowDialog();

            string filename = null;

            if (result == true)
            {
                filename = dialog.FileName;
                WriteToFile(filename, str);
            }

            return filename;
        }

        public static string OpenFileDialog(out string filename)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                filename = ofd.FileName;
                return ReadFromFile(filename);
            }

            filename = null;

            return "";
        }
    }
}
