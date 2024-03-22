using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace SMTPClient
{
    public partial class Form1 : Form
    {
        private List<string> files;

        public Form1()
        {
            InitializeComponent();

            files = new List<string>();

            SenderMail.Text = "Hitev.v.v@mail.ru";
            PasswordKey.Text = "fdz4ccbT9sxUVV0fzKie";
            ReceiverMail.Text = "Hitevvv@gmail.com";
            Header.Text = "Тема письма";
            Letter.Text = "Текст письма";
            Server.Text = "smtp.mail.ru";
            Port.Text = "587";
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            using (MailMessage mail = new MailMessage(SenderMail.Text, ReceiverMail.Text))
            {
                mail.Subject = Header.Text;
                mail.Body = Letter.Text;
                foreach (var file in files)
                {
                    mail.Attachments.Add(new Attachment(file));
                }

                string smtp_server = Server.Text;
                int port = int.Parse(Port.Text.Trim());

                using (SmtpClient smtp = new SmtpClient(smtp_server, port))
                {
                    smtp.Credentials = new NetworkCredential(SenderMail.Text, PasswordKey.Text);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private void AttachButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                files.Add(ofd.FileName);
            }
            Files.Text = "";
            foreach (string file in files)
            {
                Files.Text = Files.Text + file + "\r\n";
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Letter.Text = "";
            Files.Text = "";
            files.Clear();
        }
    }
}