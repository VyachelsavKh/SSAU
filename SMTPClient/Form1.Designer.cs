namespace SMTPClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SenderMail = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.Server = new System.Windows.Forms.TextBox();
            this.PasswordKey = new System.Windows.Forms.TextBox();
            this.ReceiverMail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Header = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Letter = new System.Windows.Forms.RichTextBox();
            this.Files = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AttachButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Отправитель:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(79, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ключ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(64, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Сервер:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(79, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Порт:";
            // 
            // SenderMail
            // 
            this.SenderMail.Location = new System.Drawing.Point(157, 13);
            this.SenderMail.Name = "SenderMail";
            this.SenderMail.Size = new System.Drawing.Size(177, 22);
            this.SenderMail.TabIndex = 4;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(157, 106);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(177, 22);
            this.Port.TabIndex = 5;
            // 
            // Server
            // 
            this.Server.Location = new System.Drawing.Point(157, 75);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(177, 22);
            this.Server.TabIndex = 6;
            // 
            // PasswordKey
            // 
            this.PasswordKey.Location = new System.Drawing.Point(157, 44);
            this.PasswordKey.Name = "PasswordKey";
            this.PasswordKey.Size = new System.Drawing.Size(177, 22);
            this.PasswordKey.TabIndex = 7;
            // 
            // ReceiverMail
            // 
            this.ReceiverMail.Location = new System.Drawing.Point(487, 13);
            this.ReceiverMail.Name = "ReceiverMail";
            this.ReceiverMail.Size = new System.Drawing.Size(177, 22);
            this.ReceiverMail.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(356, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Получатель:";
            // 
            // Header
            // 
            this.Header.Location = new System.Drawing.Point(487, 44);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(448, 22);
            this.Header.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(368, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Заголовок:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(393, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Письмо:";
            // 
            // Letter
            // 
            this.Letter.Location = new System.Drawing.Point(487, 75);
            this.Letter.Name = "Letter";
            this.Letter.Size = new System.Drawing.Size(448, 183);
            this.Letter.TabIndex = 14;
            this.Letter.Text = "";
            // 
            // Files
            // 
            this.Files.Location = new System.Drawing.Point(487, 267);
            this.Files.Name = "Files";
            this.Files.Size = new System.Drawing.Size(448, 183);
            this.Files.TabIndex = 16;
            this.Files.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(399, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Файлы:";
            // 
            // AttachButton
            // 
            this.AttachButton.Location = new System.Drawing.Point(487, 470);
            this.AttachButton.Name = "AttachButton";
            this.AttachButton.Size = new System.Drawing.Size(142, 38);
            this.AttachButton.TabIndex = 17;
            this.AttachButton.Text = "Прикрепить";
            this.AttachButton.UseVisualStyleBackColor = true;
            this.AttachButton.Click += new System.EventHandler(this.AttachButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(660, 470);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(142, 38);
            this.ClearButton.TabIndex = 18;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(487, 524);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(315, 38);
            this.SendButton.TabIndex = 19;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 585);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.AttachButton);
            this.Controls.Add(this.Files);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Letter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ReceiverMail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PasswordKey);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.SenderMail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "SMTP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SenderMail;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.TextBox Server;
        private System.Windows.Forms.TextBox PasswordKey;
        private System.Windows.Forms.TextBox ReceiverMail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Header;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox Letter;
        private System.Windows.Forms.RichTextBox Files;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button AttachButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button SendButton;
    }
}

