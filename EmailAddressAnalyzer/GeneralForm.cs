using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmailAddressAnalyzer
{
    public partial class GeneralForm : Form
    {
        public GeneralForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Написать программу синтаксического анализа адреса электронной почты ...");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа разработана ...");
        }

        private void GeneralForm_Load(object sender, EventArgs e)
        {

        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            ListBoxOfDomains.Items.Clear();
            Result Res = CheckEmailAddress.Check(EmailTextBox.Text + " ");
            ErrLabel.Text = Res.ErrMessage;
            if (Res.ErrPosition < 0)
            {
                ErrLabel.ForeColor = Color.Black;
                foreach (string DomainName in Res.ListOfDomains)
                {
                    ListBoxOfDomains.Items.Add(DomainName);
                }
            }
            else
            {
                ErrLabel.ForeColor = Color.Red;
                EmailTextBox.SelectionLength = 1;
                EmailTextBox.SelectionStart = Res.ErrPosition;
                EmailTextBox.Focus();
            }
        }
    }
}
