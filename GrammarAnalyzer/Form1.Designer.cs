namespace GrammarAnalyzer
{
    partial class Grammar_analyzer
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
            this.Input_string = new System.Windows.Forms.TextBox();
            this.Input = new System.Windows.Forms.Label();
            this.Check_button = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.Label();
            this.Output_string = new System.Windows.Forms.Label();
            this.Grammar = new System.Windows.Forms.Label();
            this.Grammar_file = new System.Windows.Forms.Label();
            this.Semantic = new System.Windows.Forms.Label();
            this.Semantic_button = new System.Windows.Forms.Button();
            this.Grammar_error = new System.Windows.Forms.Label();
            this.Grammar_errors = new System.Windows.Forms.Label();
            this.Input_state = new System.Windows.Forms.Label();
            this.Output_state = new System.Windows.Forms.Label();
            this.Input_state_string = new System.Windows.Forms.TextBox();
            this.Output_state_string = new System.Windows.Forms.TextBox();
            this.Semantic_str = new System.Windows.Forms.Label();
            this.Semantic_out = new System.Windows.Forms.Label();
            this.Generate_Code = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Input_string
            // 
            this.Input_string.Location = new System.Drawing.Point(150, 12);
            this.Input_string.Name = "Input_string";
            this.Input_string.Size = new System.Drawing.Size(707, 22);
            this.Input_string.TabIndex = 0;
            // 
            // Input
            // 
            this.Input.AutoSize = true;
            this.Input.Location = new System.Drawing.Point(15, 15);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(115, 16);
            this.Input.TabIndex = 1;
            this.Input.Text = "Входная строка :";
            // 
            // Check_button
            // 
            this.Check_button.Location = new System.Drawing.Point(863, 8);
            this.Check_button.Name = "Check_button";
            this.Check_button.Size = new System.Drawing.Size(111, 23);
            this.Check_button.TabIndex = 2;
            this.Check_button.Text = "Проверить";
            this.Check_button.UseVisualStyleBackColor = true;
            this.Check_button.Click += new System.EventHandler(this.Check_button_Click);
            // 
            // Output
            // 
            this.Output.AutoSize = true;
            this.Output.Location = new System.Drawing.Point(15, 78);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(92, 16);
            this.Output.TabIndex = 3;
            this.Output.Text = "Грамматика :";
            // 
            // Output_string
            // 
            this.Output_string.AutoSize = true;
            this.Output_string.Location = new System.Drawing.Point(113, 78);
            this.Output_string.Name = "Output_string";
            this.Output_string.Size = new System.Drawing.Size(0, 16);
            this.Output_string.TabIndex = 4;
            // 
            // Grammar
            // 
            this.Grammar.AutoSize = true;
            this.Grammar.Location = new System.Drawing.Point(12, 195);
            this.Grammar.Name = "Grammar";
            this.Grammar.Size = new System.Drawing.Size(92, 16);
            this.Grammar.TabIndex = 5;
            this.Grammar.Text = "Грамматика :";
            // 
            // Grammar_file
            // 
            this.Grammar_file.AutoSize = true;
            this.Grammar_file.Location = new System.Drawing.Point(124, 195);
            this.Grammar_file.Name = "Grammar_file";
            this.Grammar_file.Size = new System.Drawing.Size(0, 16);
            this.Grammar_file.TabIndex = 6;
            // 
            // Semantic
            // 
            this.Semantic.AutoSize = true;
            this.Semantic.Location = new System.Drawing.Point(735, 81);
            this.Semantic.Name = "Semantic";
            this.Semantic.Size = new System.Drawing.Size(143, 16);
            this.Semantic.TabIndex = 7;
            this.Semantic.Text = "Проверка сематики :";
            // 
            // Semantic_button
            // 
            this.Semantic_button.Location = new System.Drawing.Point(895, 78);
            this.Semantic_button.Name = "Semantic_button";
            this.Semantic_button.Size = new System.Drawing.Size(75, 23);
            this.Semantic_button.TabIndex = 8;
            this.Semantic_button.Text = "Выкл";
            this.Semantic_button.UseVisualStyleBackColor = true;
            this.Semantic_button.Click += new System.EventHandler(this.Semantic_button_Click);
            // 
            // Grammar_error
            // 
            this.Grammar_error.AutoSize = true;
            this.Grammar_error.Location = new System.Drawing.Point(12, 275);
            this.Grammar_error.Name = "Grammar_error";
            this.Grammar_error.Size = new System.Drawing.Size(0, 16);
            this.Grammar_error.TabIndex = 9;
            // 
            // Grammar_errors
            // 
            this.Grammar_errors.AutoSize = true;
            this.Grammar_errors.Location = new System.Drawing.Point(12, 249);
            this.Grammar_errors.Name = "Grammar_errors";
            this.Grammar_errors.Size = new System.Drawing.Size(152, 16);
            this.Grammar_errors.TabIndex = 10;
            this.Grammar_errors.Text = "Ошибки в грамматике:";
            // 
            // Input_state
            // 
            this.Input_state.AutoSize = true;
            this.Input_state.Location = new System.Drawing.Point(15, 49);
            this.Input_state.Name = "Input_state";
            this.Input_state.Size = new System.Drawing.Size(139, 16);
            this.Input_state.TabIndex = 11;
            this.Input_state.Text = "Входное состояние: ";
            // 
            // Output_state
            // 
            this.Output_state.AutoSize = true;
            this.Output_state.Location = new System.Drawing.Point(358, 49);
            this.Output_state.Name = "Output_state";
            this.Output_state.Size = new System.Drawing.Size(148, 16);
            this.Output_state.TabIndex = 12;
            this.Output_state.Text = "Выходное состояние: ";
            // 
            // Input_state_string
            // 
            this.Input_state_string.Location = new System.Drawing.Point(160, 46);
            this.Input_state_string.Name = "Input_state_string";
            this.Input_state_string.Size = new System.Drawing.Size(191, 22);
            this.Input_state_string.TabIndex = 13;
            this.Input_state_string.Text = "S";
            this.Input_state_string.TextChanged += new System.EventHandler(this.Input_state_string_TextChanged);
            // 
            // Output_state_string
            // 
            this.Output_state_string.Location = new System.Drawing.Point(512, 46);
            this.Output_state_string.Name = "Output_state_string";
            this.Output_state_string.Size = new System.Drawing.Size(191, 22);
            this.Output_state_string.TabIndex = 14;
            this.Output_state_string.Text = "F";
            this.Output_state_string.TextChanged += new System.EventHandler(this.Output_state_string_TextChanged);
            // 
            // Semantic_str
            // 
            this.Semantic_str.AutoSize = true;
            this.Semantic_str.Location = new System.Drawing.Point(509, 81);
            this.Semantic_str.Name = "Semantic_str";
            this.Semantic_str.Size = new System.Drawing.Size(85, 16);
            this.Semantic_str.TabIndex = 15;
            this.Semantic_str.Text = "Семантика :";
            // 
            // Semantic_out
            // 
            this.Semantic_out.AutoSize = true;
            this.Semantic_out.Location = new System.Drawing.Point(506, 105);
            this.Semantic_out.Name = "Semantic_out";
            this.Semantic_out.Size = new System.Drawing.Size(93, 16);
            this.Semantic_out.TabIndex = 16;
            this.Semantic_out.Text = "Проверяется";
            // 
            // Generate_Code
            // 
            this.Generate_Code.Location = new System.Drawing.Point(9, 214);
            this.Generate_Code.Name = "Generate_Code";
            this.Generate_Code.Size = new System.Drawing.Size(175, 23);
            this.Generate_Code.TabIndex = 17;
            this.Generate_Code.Text = "Сгенерировать код";
            this.Generate_Code.UseVisualStyleBackColor = true;
            this.Generate_Code.Click += new System.EventHandler(this.Generate_Code_Click);
            // 
            // Grammar_analyzer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 489);
            this.Controls.Add(this.Generate_Code);
            this.Controls.Add(this.Semantic_out);
            this.Controls.Add(this.Semantic_str);
            this.Controls.Add(this.Output_state_string);
            this.Controls.Add(this.Input_state_string);
            this.Controls.Add(this.Output_state);
            this.Controls.Add(this.Input_state);
            this.Controls.Add(this.Grammar_errors);
            this.Controls.Add(this.Grammar_error);
            this.Controls.Add(this.Semantic_button);
            this.Controls.Add(this.Semantic);
            this.Controls.Add(this.Grammar_file);
            this.Controls.Add(this.Grammar);
            this.Controls.Add(this.Output_string);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.Check_button);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.Input_string);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Grammar_analyzer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Grammar Analyzer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grammar_analyzer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grammar_analyzer_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Input_string;
        private System.Windows.Forms.Label Input;
        private System.Windows.Forms.Button Check_button;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.Label Output_string;
        private System.Windows.Forms.Label Grammar;
        private System.Windows.Forms.Label Grammar_file;
        private System.Windows.Forms.Label Semantic;
        private System.Windows.Forms.Button Semantic_button;
        private System.Windows.Forms.Label Grammar_error;
        private System.Windows.Forms.Label Grammar_errors;
        private System.Windows.Forms.Label Input_state;
        private System.Windows.Forms.Label Output_state;
        private System.Windows.Forms.TextBox Input_state_string;
        private System.Windows.Forms.TextBox Output_state_string;
        private System.Windows.Forms.Label Semantic_str;
        private System.Windows.Forms.Label Semantic_out;
        private System.Windows.Forms.Button Generate_Code;
    }
}

