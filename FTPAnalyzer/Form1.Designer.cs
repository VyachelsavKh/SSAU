namespace FTPAnalyzer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.FTPAdress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FTPPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FTPLogin = new System.Windows.Forms.TextBox();
            this.ResultTree = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StartAnalyze = new System.Windows.Forms.Button();
            this.FilesSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FTPAdress
            // 
            this.FTPAdress.Location = new System.Drawing.Point(139, 22);
            this.FTPAdress.Name = "FTPAdress";
            this.FTPAdress.Size = new System.Drawing.Size(192, 22);
            this.FTPAdress.TabIndex = 0;
            this.FTPAdress.Text = "ftp://91.222.128.11";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Адрес сервера:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль:";
            // 
            // FTPPassword
            // 
            this.FTPPassword.Location = new System.Drawing.Point(139, 92);
            this.FTPPassword.Name = "FTPPassword";
            this.FTPPassword.Size = new System.Drawing.Size(192, 22);
            this.FTPPassword.TabIndex = 2;
            this.FTPPassword.Text = "12345";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Логин:";
            // 
            // FTPLogin
            // 
            this.FTPLogin.Location = new System.Drawing.Point(139, 58);
            this.FTPLogin.Name = "FTPLogin";
            this.FTPLogin.Size = new System.Drawing.Size(192, 22);
            this.FTPLogin.TabIndex = 4;
            this.FTPLogin.Text = "testftp_guest";
            // 
            // ResultTree
            // 
            this.ResultTree.Location = new System.Drawing.Point(16, 157);
            this.ResultTree.Name = "ResultTree";
            this.ResultTree.Size = new System.Drawing.Size(497, 548);
            this.ResultTree.TabIndex = 7;
            this.ResultTree.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Результат:";
            // 
            // StartAnalyze
            // 
            this.StartAnalyze.Location = new System.Drawing.Point(379, 22);
            this.StartAnalyze.Name = "StartAnalyze";
            this.StartAnalyze.Size = new System.Drawing.Size(109, 58);
            this.StartAnalyze.TabIndex = 9;
            this.StartAnalyze.Text = "Анализ";
            this.StartAnalyze.UseVisualStyleBackColor = true;
            this.StartAnalyze.Click += new System.EventHandler(this.StartAnalyze_Click);
            // 
            // FilesSize
            // 
            this.FilesSize.AutoSize = true;
            this.FilesSize.Location = new System.Drawing.Point(358, 98);
            this.FilesSize.Name = "FilesSize";
            this.FilesSize.Size = new System.Drawing.Size(155, 16);
            this.FilesSize.TabIndex = 10;
            this.FilesSize.Text = "Общий объём файлов: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 717);
            this.Controls.Add(this.FilesSize);
            this.Controls.Add(this.StartAnalyze);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ResultTree);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FTPLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FTPPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FTPAdress);
            this.Name = "Form1";
            this.Text = "FTP Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FTPAdress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FTPPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FTPLogin;
        private System.Windows.Forms.RichTextBox ResultTree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button StartAnalyze;
        private System.Windows.Forms.Label FilesSize;
    }
}

