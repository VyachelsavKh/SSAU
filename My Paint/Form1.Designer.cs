namespace My_Paint
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.Clear_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Mixed_color_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BColor_value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GColor_value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RColor_value = new System.Windows.Forms.TextBox();
            this.Color12_button = new System.Windows.Forms.Button();
            this.Color11_button = new System.Windows.Forms.Button();
            this.Color10_button = new System.Windows.Forms.Button();
            this.Color9_button = new System.Windows.Forms.Button();
            this.Color8_button = new System.Windows.Forms.Button();
            this.Color7_button = new System.Windows.Forms.Button();
            this.Color6_button = new System.Windows.Forms.Button();
            this.Color5_button = new System.Windows.Forms.Button();
            this.Color4_button = new System.Windows.Forms.Button();
            this.Color3_button = new System.Windows.Forms.Button();
            this.Color2_button = new System.Windows.Forms.Button();
            this.Color1_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Thickness_bar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.Save_button = new System.Windows.Forms.Button();
            this.Pen_button = new System.Windows.Forms.Button();
            this.Line_button = new System.Windows.Forms.Button();
            this.Circle_button = new System.Windows.Forms.Button();
            this.Rectangle_button = new System.Windows.Forms.Button();
            this.Fill_button = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Bezie_button = new System.Windows.Forms.Button();
            this.FillModify_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Thickness_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1254, 853);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.UseWaitCursor = true;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // Clear_button
            // 
            this.Clear_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Clear_button.Location = new System.Drawing.Point(1262, 263);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(132, 66);
            this.Clear_button.TabIndex = 1;
            this.Clear_button.Text = "Clear";
            this.Clear_button.UseVisualStyleBackColor = true;
            this.Clear_button.UseWaitCursor = true;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Mixed_color_button);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BColor_value);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.GColor_value);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.RColor_value);
            this.panel1.Controls.Add(this.Color12_button);
            this.panel1.Controls.Add(this.Color11_button);
            this.panel1.Controls.Add(this.Color10_button);
            this.panel1.Controls.Add(this.Color9_button);
            this.panel1.Controls.Add(this.Color8_button);
            this.panel1.Controls.Add(this.Color7_button);
            this.panel1.Controls.Add(this.Color6_button);
            this.panel1.Controls.Add(this.Color5_button);
            this.panel1.Controls.Add(this.Color4_button);
            this.panel1.Controls.Add(this.Color3_button);
            this.panel1.Controls.Add(this.Color2_button);
            this.panel1.Controls.Add(this.Color1_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1254, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 156);
            this.panel1.TabIndex = 2;
            this.panel1.UseWaitCursor = true;
            // 
            // Mixed_color_button
            // 
            this.Mixed_color_button.BackColor = System.Drawing.Color.Black;
            this.Mixed_color_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Mixed_color_button.Location = new System.Drawing.Point(144, 105);
            this.Mixed_color_button.Name = "Mixed_color_button";
            this.Mixed_color_button.Size = new System.Drawing.Size(132, 40);
            this.Mixed_color_button.TabIndex = 18;
            this.Mixed_color_button.UseVisualStyleBackColor = false;
            this.Mixed_color_button.UseWaitCursor = true;
            this.Mixed_color_button.Click += new System.EventHandler(this.Mixed_color_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(99, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 22);
            this.label3.TabIndex = 17;
            this.label3.Text = "B:";
            this.label3.UseWaitCursor = true;
            // 
            // BColor_value
            // 
            this.BColor_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BColor_value.Location = new System.Drawing.Point(99, 117);
            this.BColor_value.Name = "BColor_value";
            this.BColor_value.Size = new System.Drawing.Size(39, 28);
            this.BColor_value.TabIndex = 16;
            this.BColor_value.Text = "0";
            this.BColor_value.UseWaitCursor = true;
            this.BColor_value.TextChanged += new System.EventHandler(this.BColor_value_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(53, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "G:";
            this.label2.UseWaitCursor = true;
            // 
            // GColor_value
            // 
            this.GColor_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GColor_value.Location = new System.Drawing.Point(53, 117);
            this.GColor_value.Name = "GColor_value";
            this.GColor_value.Size = new System.Drawing.Size(39, 28);
            this.GColor_value.TabIndex = 14;
            this.GColor_value.Text = "0";
            this.GColor_value.UseWaitCursor = true;
            this.GColor_value.TextChanged += new System.EventHandler(this.GColor_value_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "R:";
            this.label1.UseWaitCursor = true;
            // 
            // RColor_value
            // 
            this.RColor_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RColor_value.Location = new System.Drawing.Point(8, 117);
            this.RColor_value.Name = "RColor_value";
            this.RColor_value.Size = new System.Drawing.Size(39, 28);
            this.RColor_value.TabIndex = 12;
            this.RColor_value.Text = "0";
            this.RColor_value.UseWaitCursor = true;
            this.RColor_value.TextChanged += new System.EventHandler(this.RColor_value_TextChanged);
            // 
            // Color12_button
            // 
            this.Color12_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Color12_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color12_button.Location = new System.Drawing.Point(236, 49);
            this.Color12_button.Name = "Color12_button";
            this.Color12_button.Size = new System.Drawing.Size(40, 40);
            this.Color12_button.TabIndex = 11;
            this.Color12_button.UseVisualStyleBackColor = false;
            this.Color12_button.UseWaitCursor = true;
            this.Color12_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color11_button
            // 
            this.Color11_button.BackColor = System.Drawing.Color.Purple;
            this.Color11_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color11_button.Location = new System.Drawing.Point(190, 49);
            this.Color11_button.Name = "Color11_button";
            this.Color11_button.Size = new System.Drawing.Size(40, 40);
            this.Color11_button.TabIndex = 10;
            this.Color11_button.UseVisualStyleBackColor = false;
            this.Color11_button.UseWaitCursor = true;
            this.Color11_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color10_button
            // 
            this.Color10_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Color10_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color10_button.Location = new System.Drawing.Point(144, 49);
            this.Color10_button.Name = "Color10_button";
            this.Color10_button.Size = new System.Drawing.Size(40, 40);
            this.Color10_button.TabIndex = 9;
            this.Color10_button.UseVisualStyleBackColor = false;
            this.Color10_button.UseWaitCursor = true;
            this.Color10_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color9_button
            // 
            this.Color9_button.BackColor = System.Drawing.Color.Fuchsia;
            this.Color9_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color9_button.Location = new System.Drawing.Point(98, 49);
            this.Color9_button.Name = "Color9_button";
            this.Color9_button.Size = new System.Drawing.Size(40, 40);
            this.Color9_button.TabIndex = 8;
            this.Color9_button.UseVisualStyleBackColor = false;
            this.Color9_button.UseWaitCursor = true;
            this.Color9_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color8_button
            // 
            this.Color8_button.BackColor = System.Drawing.Color.Blue;
            this.Color8_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color8_button.Location = new System.Drawing.Point(52, 49);
            this.Color8_button.Name = "Color8_button";
            this.Color8_button.Size = new System.Drawing.Size(40, 40);
            this.Color8_button.TabIndex = 7;
            this.Color8_button.UseVisualStyleBackColor = false;
            this.Color8_button.UseWaitCursor = true;
            this.Color8_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color7_button
            // 
            this.Color7_button.BackColor = System.Drawing.Color.Black;
            this.Color7_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color7_button.Location = new System.Drawing.Point(6, 49);
            this.Color7_button.Name = "Color7_button";
            this.Color7_button.Size = new System.Drawing.Size(40, 40);
            this.Color7_button.TabIndex = 6;
            this.Color7_button.UseVisualStyleBackColor = false;
            this.Color7_button.UseWaitCursor = true;
            this.Color7_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color6_button
            // 
            this.Color6_button.BackColor = System.Drawing.Color.Cyan;
            this.Color6_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color6_button.Location = new System.Drawing.Point(236, 3);
            this.Color6_button.Name = "Color6_button";
            this.Color6_button.Size = new System.Drawing.Size(40, 40);
            this.Color6_button.TabIndex = 5;
            this.Color6_button.UseVisualStyleBackColor = false;
            this.Color6_button.UseWaitCursor = true;
            this.Color6_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color5_button
            // 
            this.Color5_button.BackColor = System.Drawing.Color.Lime;
            this.Color5_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color5_button.Location = new System.Drawing.Point(190, 3);
            this.Color5_button.Name = "Color5_button";
            this.Color5_button.Size = new System.Drawing.Size(40, 40);
            this.Color5_button.TabIndex = 4;
            this.Color5_button.UseVisualStyleBackColor = false;
            this.Color5_button.UseWaitCursor = true;
            this.Color5_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color4_button
            // 
            this.Color4_button.BackColor = System.Drawing.Color.Yellow;
            this.Color4_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color4_button.Location = new System.Drawing.Point(144, 3);
            this.Color4_button.Name = "Color4_button";
            this.Color4_button.Size = new System.Drawing.Size(40, 40);
            this.Color4_button.TabIndex = 3;
            this.Color4_button.UseVisualStyleBackColor = false;
            this.Color4_button.UseWaitCursor = true;
            this.Color4_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color3_button
            // 
            this.Color3_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Color3_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color3_button.Location = new System.Drawing.Point(98, 3);
            this.Color3_button.Name = "Color3_button";
            this.Color3_button.Size = new System.Drawing.Size(40, 40);
            this.Color3_button.TabIndex = 2;
            this.Color3_button.UseVisualStyleBackColor = false;
            this.Color3_button.UseWaitCursor = true;
            this.Color3_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color2_button
            // 
            this.Color2_button.BackColor = System.Drawing.Color.Red;
            this.Color2_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color2_button.Location = new System.Drawing.Point(52, 3);
            this.Color2_button.Name = "Color2_button";
            this.Color2_button.Size = new System.Drawing.Size(40, 40);
            this.Color2_button.TabIndex = 1;
            this.Color2_button.UseVisualStyleBackColor = false;
            this.Color2_button.UseWaitCursor = true;
            this.Color2_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // Color1_button
            // 
            this.Color1_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Color1_button.Location = new System.Drawing.Point(6, 3);
            this.Color1_button.Name = "Color1_button";
            this.Color1_button.Size = new System.Drawing.Size(40, 40);
            this.Color1_button.TabIndex = 0;
            this.Color1_button.UseVisualStyleBackColor = true;
            this.Color1_button.UseWaitCursor = true;
            this.Color1_button.Click += new System.EventHandler(this.Color_button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Thickness_bar);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1254, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 101);
            this.panel2.TabIndex = 3;
            this.panel2.UseWaitCursor = true;
            // 
            // Thickness_bar
            // 
            this.Thickness_bar.Location = new System.Drawing.Point(14, 38);
            this.Thickness_bar.Minimum = 1;
            this.Thickness_bar.Name = "Thickness_bar";
            this.Thickness_bar.Size = new System.Drawing.Size(264, 56);
            this.Thickness_bar.TabIndex = 1;
            this.Thickness_bar.UseWaitCursor = true;
            this.Thickness_bar.Value = 1;
            this.Thickness_bar.Scroll += new System.EventHandler(this.Thickness_bar_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(24, 3);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(228, 32);
            this.label4.TabIndex = 0;
            this.label4.Text = "Выбор толщины";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.UseWaitCursor = true;
            // 
            // Save_button
            // 
            this.Save_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Save_button.Location = new System.Drawing.Point(1400, 263);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(132, 66);
            this.Save_button.TabIndex = 4;
            this.Save_button.Text = "Save";
            this.Save_button.UseVisualStyleBackColor = true;
            this.Save_button.UseWaitCursor = true;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // Pen_button
            // 
            this.Pen_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Pen_button.Location = new System.Drawing.Point(1262, 335);
            this.Pen_button.Name = "Pen_button";
            this.Pen_button.Size = new System.Drawing.Size(132, 66);
            this.Pen_button.TabIndex = 5;
            this.Pen_button.Text = "Pen";
            this.Pen_button.UseVisualStyleBackColor = true;
            this.Pen_button.UseWaitCursor = true;
            this.Pen_button.Click += new System.EventHandler(this.Pen_button_Click);
            // 
            // Line_button
            // 
            this.Line_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Line_button.Location = new System.Drawing.Point(1400, 335);
            this.Line_button.Name = "Line_button";
            this.Line_button.Size = new System.Drawing.Size(132, 66);
            this.Line_button.TabIndex = 6;
            this.Line_button.Text = "Line";
            this.Line_button.UseVisualStyleBackColor = true;
            this.Line_button.UseWaitCursor = true;
            this.Line_button.Click += new System.EventHandler(this.Line_button_Click);
            // 
            // Circle_button
            // 
            this.Circle_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Circle_button.Location = new System.Drawing.Point(1262, 407);
            this.Circle_button.Name = "Circle_button";
            this.Circle_button.Size = new System.Drawing.Size(132, 66);
            this.Circle_button.TabIndex = 7;
            this.Circle_button.Text = "Circle";
            this.Circle_button.UseVisualStyleBackColor = true;
            this.Circle_button.UseWaitCursor = true;
            this.Circle_button.Click += new System.EventHandler(this.Circle_button_Click);
            // 
            // Rectangle_button
            // 
            this.Rectangle_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Rectangle_button.Location = new System.Drawing.Point(1400, 407);
            this.Rectangle_button.Name = "Rectangle_button";
            this.Rectangle_button.Size = new System.Drawing.Size(132, 66);
            this.Rectangle_button.TabIndex = 8;
            this.Rectangle_button.Text = "Rect";
            this.Rectangle_button.UseVisualStyleBackColor = true;
            this.Rectangle_button.UseWaitCursor = true;
            this.Rectangle_button.Click += new System.EventHandler(this.Rectangle_button_Click);
            // 
            // Fill_button
            // 
            this.Fill_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Fill_button.Location = new System.Drawing.Point(1262, 479);
            this.Fill_button.Name = "Fill_button";
            this.Fill_button.Size = new System.Drawing.Size(132, 66);
            this.Fill_button.TabIndex = 9;
            this.Fill_button.Text = "Fill";
            this.Fill_button.UseVisualStyleBackColor = true;
            this.Fill_button.UseWaitCursor = true;
            this.Fill_button.Click += new System.EventHandler(this.Fill_button_Click);
            // 
            // Bezie_button
            // 
            this.Bezie_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Bezie_button.Location = new System.Drawing.Point(1260, 551);
            this.Bezie_button.Name = "Bezie_button";
            this.Bezie_button.Size = new System.Drawing.Size(132, 66);
            this.Bezie_button.TabIndex = 10;
            this.Bezie_button.Text = "Bezie";
            this.Bezie_button.UseVisualStyleBackColor = true;
            this.Bezie_button.UseWaitCursor = true;
            this.Bezie_button.Click += new System.EventHandler(this.Bezie_button_Click);
            // 
            // FillModify_button
            // 
            this.FillModify_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillModify_button.Location = new System.Drawing.Point(1400, 479);
            this.FillModify_button.Name = "FillModify_button";
            this.FillModify_button.Size = new System.Drawing.Size(132, 66);
            this.FillModify_button.TabIndex = 11;
            this.FillModify_button.Text = "FillM";
            this.FillModify_button.UseVisualStyleBackColor = true;
            this.FillModify_button.UseWaitCursor = true;
            this.FillModify_button.Click += new System.EventHandler(this.FillModify_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.FillModify_button);
            this.Controls.Add(this.Bezie_button);
            this.Controls.Add(this.Fill_button);
            this.Controls.Add(this.Rectangle_button);
            this.Controls.Add(this.Circle_button);
            this.Controls.Add(this.Line_button);
            this.Controls.Add(this.Pen_button);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Clear_button);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(1600, 900);
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Paint";
            this.UseWaitCursor = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Thickness_bar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Color12_button;
        private System.Windows.Forms.Button Color11_button;
        private System.Windows.Forms.Button Color10_button;
        private System.Windows.Forms.Button Color9_button;
        private System.Windows.Forms.Button Color8_button;
        private System.Windows.Forms.Button Color7_button;
        private System.Windows.Forms.Button Color6_button;
        private System.Windows.Forms.Button Color5_button;
        private System.Windows.Forms.Button Color4_button;
        private System.Windows.Forms.Button Color3_button;
        private System.Windows.Forms.Button Color2_button;
        private System.Windows.Forms.Button Color1_button;
        private System.Windows.Forms.TextBox RColor_value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox BColor_value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox GColor_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Mixed_color_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TrackBar Thickness_bar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Button Pen_button;
        private System.Windows.Forms.Button Line_button;
        private System.Windows.Forms.Button Circle_button;
        private System.Windows.Forms.Button Rectangle_button;
        private System.Windows.Forms.Button Fill_button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button Bezie_button;
        private System.Windows.Forms.Button FillModify_button;
    }
}

