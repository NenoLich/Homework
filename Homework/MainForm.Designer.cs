namespace Homework
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.homework_label = new System.Windows.Forms.Label();
            this.homework_panel = new System.Windows.Forms.Panel();
            this.homework_next_Button = new System.Windows.Forms.Button();
            this.homeworks_comboBox = new System.Windows.Forms.ComboBox();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.method_panel = new System.Windows.Forms.Panel();
            this.method_back_Button = new System.Windows.Forms.Button();
            this.method_next_Button = new System.Windows.Forms.Button();
            this.method_comboBox = new System.Windows.Forms.ComboBox();
            this.method_label = new System.Windows.Forms.Label();
            this.parameters_panel = new System.Windows.Forms.Panel();
            this.apply_Button = new System.Windows.Forms.Button();
            this.parameters_textBox = new System.Windows.Forms.TextBox();
            this.parameters_back_button = new System.Windows.Forms.Button();
            this.parameters_next_button = new System.Windows.Forms.Button();
            this.parameters_comboBox = new System.Windows.Forms.ComboBox();
            this.parameters_label = new System.Windows.Forms.Label();
            this.homework_panel.SuspendLayout();
            this.method_panel.SuspendLayout();
            this.parameters_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // homework_label
            // 
            this.homework_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.homework_label.Location = new System.Drawing.Point(25, 26);
            this.homework_label.Name = "homework_label";
            this.homework_label.Size = new System.Drawing.Size(175, 13);
            this.homework_label.TabIndex = 0;
            this.homework_label.Text = "Выберите домашнее задание:";
            this.homework_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homework_panel
            // 
            this.homework_panel.Controls.Add(this.homework_next_Button);
            this.homework_panel.Controls.Add(this.homeworks_comboBox);
            this.homework_panel.Controls.Add(this.homework_label);
            this.homework_panel.Location = new System.Drawing.Point(40, 54);
            this.homework_panel.Name = "homework_panel";
            this.homework_panel.Size = new System.Drawing.Size(231, 175);
            this.homework_panel.TabIndex = 1;
            // 
            // homework_next_Button
            // 
            this.homework_next_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.homework_next_Button.Location = new System.Drawing.Point(83, 125);
            this.homework_next_Button.Name = "homework_next_Button";
            this.homework_next_Button.Size = new System.Drawing.Size(75, 23);
            this.homework_next_Button.TabIndex = 2;
            this.homework_next_Button.Text = "Дальше";
            this.homework_next_Button.UseVisualStyleBackColor = true;
            this.homework_next_Button.Click += new System.EventHandler(this.next_Button_Click);
            // 
            // homeworks_comboBox
            // 
            this.homeworks_comboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.homeworks_comboBox.DisplayMember = "name";
            this.homeworks_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.homeworks_comboBox.FormattingEnabled = true;
            this.homeworks_comboBox.Location = new System.Drawing.Point(28, 74);
            this.homeworks_comboBox.Name = "homeworks_comboBox";
            this.homeworks_comboBox.Size = new System.Drawing.Size(172, 21);
            this.homeworks_comboBox.TabIndex = 1;
            // 
            // Exit_Button
            // 
            this.Exit_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Exit_Button.Location = new System.Drawing.Point(754, 272);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(75, 23);
            this.Exit_Button.TabIndex = 2;
            this.Exit_Button.Text = "Выход";
            this.Exit_Button.UseVisualStyleBackColor = true;
            this.Exit_Button.Click += new System.EventHandler(this.Exit_Button_Click);
            // 
            // method_panel
            // 
            this.method_panel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.method_panel.Controls.Add(this.method_back_Button);
            this.method_panel.Controls.Add(this.method_next_Button);
            this.method_panel.Controls.Add(this.method_comboBox);
            this.method_panel.Controls.Add(this.method_label);
            this.method_panel.Enabled = false;
            this.method_panel.Location = new System.Drawing.Point(330, 54);
            this.method_panel.Name = "method_panel";
            this.method_panel.Size = new System.Drawing.Size(231, 175);
            this.method_panel.TabIndex = 3;
            this.method_panel.Visible = false;
            // 
            // method_back_Button
            // 
            this.method_back_Button.Location = new System.Drawing.Point(21, 125);
            this.method_back_Button.Name = "method_back_Button";
            this.method_back_Button.Size = new System.Drawing.Size(75, 23);
            this.method_back_Button.TabIndex = 3;
            this.method_back_Button.Text = "Назад";
            this.method_back_Button.UseVisualStyleBackColor = true;
            this.method_back_Button.Click += new System.EventHandler(this.method_back_Button_Click);
            // 
            // method_next_Button
            // 
            this.method_next_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.method_next_Button.Location = new System.Drawing.Point(137, 125);
            this.method_next_Button.Name = "method_next_Button";
            this.method_next_Button.Size = new System.Drawing.Size(75, 23);
            this.method_next_Button.TabIndex = 2;
            this.method_next_Button.Text = "Дальше";
            this.method_next_Button.UseVisualStyleBackColor = true;
            this.method_next_Button.Click += new System.EventHandler(this.method_next_Button_Click);
            // 
            // method_comboBox
            // 
            this.method_comboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.method_comboBox.DisplayMember = "name";
            this.method_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.method_comboBox.FormattingEnabled = true;
            this.method_comboBox.Location = new System.Drawing.Point(43, 74);
            this.method_comboBox.Name = "method_comboBox";
            this.method_comboBox.Size = new System.Drawing.Size(157, 21);
            this.method_comboBox.TabIndex = 1;
            // 
            // method_label
            // 
            this.method_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.method_label.AutoSize = true;
            this.method_label.Location = new System.Drawing.Point(40, 26);
            this.method_label.MaximumSize = new System.Drawing.Size(200, 20);
            this.method_label.MinimumSize = new System.Drawing.Size(160, 13);
            this.method_label.Name = "method_label";
            this.method_label.Size = new System.Drawing.Size(160, 13);
            this.method_label.TabIndex = 0;
            this.method_label.Text = "Выберите задачу:";
            this.method_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // parameters_panel
            // 
            this.parameters_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parameters_panel.Controls.Add(this.apply_Button);
            this.parameters_panel.Controls.Add(this.parameters_textBox);
            this.parameters_panel.Controls.Add(this.parameters_back_button);
            this.parameters_panel.Controls.Add(this.parameters_next_button);
            this.parameters_panel.Controls.Add(this.parameters_comboBox);
            this.parameters_panel.Controls.Add(this.parameters_label);
            this.parameters_panel.Enabled = false;
            this.parameters_panel.Location = new System.Drawing.Point(615, 54);
            this.parameters_panel.Name = "parameters_panel";
            this.parameters_panel.Size = new System.Drawing.Size(231, 175);
            this.parameters_panel.TabIndex = 4;
            this.parameters_panel.Visible = false;
            // 
            // apply_Button
            // 
            this.apply_Button.Location = new System.Drawing.Point(137, 88);
            this.apply_Button.Name = "apply_Button";
            this.apply_Button.Size = new System.Drawing.Size(75, 23);
            this.apply_Button.TabIndex = 5;
            this.apply_Button.Text = "Установить";
            this.apply_Button.UseVisualStyleBackColor = true;
            this.apply_Button.Click += new System.EventHandler(this.apply_Button_Click);
            // 
            // parameters_textBox
            // 
            this.parameters_textBox.Location = new System.Drawing.Point(21, 88);
            this.parameters_textBox.Name = "parameters_textBox";
            this.parameters_textBox.Size = new System.Drawing.Size(100, 20);
            this.parameters_textBox.TabIndex = 0;
            // 
            // parameters_back_button
            // 
            this.parameters_back_button.Location = new System.Drawing.Point(21, 125);
            this.parameters_back_button.Name = "parameters_back_button";
            this.parameters_back_button.Size = new System.Drawing.Size(75, 23);
            this.parameters_back_button.TabIndex = 3;
            this.parameters_back_button.Text = "Назад";
            this.parameters_back_button.UseVisualStyleBackColor = true;
            this.parameters_back_button.Click += new System.EventHandler(this.parameters_back_button_Click);
            // 
            // parameters_next_button
            // 
            this.parameters_next_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.parameters_next_button.Location = new System.Drawing.Point(137, 125);
            this.parameters_next_button.Name = "parameters_next_button";
            this.parameters_next_button.Size = new System.Drawing.Size(75, 23);
            this.parameters_next_button.TabIndex = 2;
            this.parameters_next_button.Text = "Дальше";
            this.parameters_next_button.UseVisualStyleBackColor = true;
            this.parameters_next_button.Click += new System.EventHandler(this.parameters_next_button_Click);
            // 
            // parameters_comboBox
            // 
            this.parameters_comboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.parameters_comboBox.DisplayMember = "name";
            this.parameters_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parameters_comboBox.FormattingEnabled = true;
            this.parameters_comboBox.Location = new System.Drawing.Point(43, 61);
            this.parameters_comboBox.Name = "parameters_comboBox";
            this.parameters_comboBox.Size = new System.Drawing.Size(157, 21);
            this.parameters_comboBox.TabIndex = 1;
            this.parameters_comboBox.SelectedIndexChanged += new System.EventHandler(this.parameters_comboBox_SelectedIndexChanged);
            // 
            // parameters_label
            // 
            this.parameters_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.parameters_label.AutoSize = true;
            this.parameters_label.Location = new System.Drawing.Point(40, 26);
            this.parameters_label.MaximumSize = new System.Drawing.Size(200, 20);
            this.parameters_label.MinimumSize = new System.Drawing.Size(160, 13);
            this.parameters_label.Name = "parameters_label";
            this.parameters_label.Size = new System.Drawing.Size(160, 13);
            this.parameters_label.TabIndex = 4;
            this.parameters_label.Text = "Установите параметры:";
            this.parameters_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AcceptButton = this.apply_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit_Button;
            this.ClientSize = new System.Drawing.Size(888, 334);
            this.Controls.Add(this.parameters_panel);
            this.Controls.Add(this.method_panel);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.homework_panel);
            this.Name = "MainMenu";
            this.Text = "Главное меню";
            this.Activated += new System.EventHandler(this.MainMenu_Activated);
            this.homework_panel.ResumeLayout(false);
            this.method_panel.ResumeLayout(false);
            this.method_panel.PerformLayout();
            this.parameters_panel.ResumeLayout(false);
            this.parameters_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label homework_label;
        private System.Windows.Forms.Panel homework_panel;
        private System.Windows.Forms.ComboBox homeworks_comboBox;
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.Button homework_next_Button;
        private System.Windows.Forms.Panel method_panel;
        private System.Windows.Forms.Button method_next_Button;
        private System.Windows.Forms.ComboBox method_comboBox;
        private System.Windows.Forms.Label method_label;
        private System.Windows.Forms.Button method_back_Button;
        private System.Windows.Forms.Panel parameters_panel;
        private System.Windows.Forms.TextBox parameters_textBox;
        private System.Windows.Forms.Button parameters_back_button;
        private System.Windows.Forms.Button parameters_next_button;
        private System.Windows.Forms.ComboBox parameters_comboBox;
        private System.Windows.Forms.Label parameters_label;
        private System.Windows.Forms.Button apply_Button;
    }
}