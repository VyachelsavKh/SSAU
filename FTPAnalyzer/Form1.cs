using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace FTPAnalyzer
{
    public partial class Form1 : Form
    {
        private string ftpUsername;
        private string ftpPassword;

        public Form1()
        {
            InitializeComponent();
        }

        class Object
        { 
            public enum Type { dir, file };
            public Type type;
            public bool access;

            public string name;
            public int size;

            public Object(string[] tokens)
            {
                if (tokens.Length < 2)
                {
                    name = tokens[0];
                    return;
                }

                if (tokens[0][0] == 'd')
                    type = Type.dir;
                else
                    type = Type.file;

                if (tokens[0][4] == 'r')
                    access = true;
                else
                    access = false;

                for (int i = 8; i < tokens.Length; i++)
                    name += tokens[i] + ' ';
                name = name.Substring(0, name.Length - 1);

                size = int.Parse(tokens[4]);
            }
        }

        private long size;

        private void AnalyzeFtp(string url, int level)
        {
            try 
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Timeout = 1000;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string ln;

                List<string> lines = new List<string>();

                while((ln = reader.ReadLine()) != null)
                    lines.Add(ln);

                response.Close();
                request.Abort();

                foreach (string line in lines)
                {
                    string[] tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Object curObject = new Object(tokens);

                    /*curObject.access = true;
                    curObject.name = tokens[3];
                    if (tokens[2] == "<DIR>")
                        curObject.type = Object.Type.dir;
                    else
                    {
                        curObject.size = int.Parse(tokens[2]);
                        curObject.type = Object.Type.file;
                    }*/

                    for (int i = 0; i < level; i++)
                        ResultTree.Text += "   ";

                    ResultTree.Text += curObject.name + "\n";

                    if (curObject.type == Object.Type.dir)
                    {
                        if (curObject.access)
                        {
                            string newUrl = url + "/" + curObject.name;

                            AnalyzeFtp(newUrl, level + 1);
                        }
                    }
                    else
                    {
                        size += curObject.size;
                    }
                }

                response.Close();
            } 
            catch { }
        }

        private void StartAnalyze_Click(object sender, EventArgs e)
        {
            ftpUsername = FTPLogin.Text;
            ftpPassword = FTPPassword.Text;

            size = 0;

            string ftp = FTPAdress.Text;

            if (!ftp.Contains("ftp://"))
                ftp = "ftp://" + ftp;

            AnalyzeFtp(ftp, 0);

            FilesSize.Text = "Общий объём файлов: \n" + size.ToString() + "\n"; 
        }
    }
}
