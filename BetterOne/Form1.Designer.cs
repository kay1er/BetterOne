﻿namespace BetterOne
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxClients = new ComboBox();
            listBoxFiles = new ListBox();
            panel1 = new Panel();
            btnPlayMusic = new Button();
            button2 = new Button();
            btnGetFiles = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxClients
            // 
            comboBoxClients.Dock = DockStyle.Top;
            comboBoxClients.FormattingEnabled = true;
            comboBoxClients.Location = new Point(0, 0);
            comboBoxClients.Name = "comboBoxClients";
            comboBoxClients.Size = new Size(455, 23);
            comboBoxClients.TabIndex = 2;
            // 
            // listBoxFiles
            // 
            listBoxFiles.Dock = DockStyle.Top;
            listBoxFiles.FormattingEnabled = true;
            listBoxFiles.ItemHeight = 15;
            listBoxFiles.Location = new Point(0, 23);
            listBoxFiles.Name = "listBoxFiles";
            listBoxFiles.Size = new Size(455, 334);
            listBoxFiles.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPlayMusic);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(btnGetFiles);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 350);
            panel1.Name = "panel1";
            panel1.Size = new Size(455, 100);
            panel1.TabIndex = 4;
            // 
            // btnPlayMusic
            // 
            btnPlayMusic.Location = new Point(148, 3);
            btnPlayMusic.Name = "btnPlayMusic";
            btnPlayMusic.Size = new Size(146, 94);
            btnPlayMusic.TabIndex = 7;
            btnPlayMusic.Text = "Play";
            btnPlayMusic.UseVisualStyleBackColor = true;
            btnPlayMusic.Click += btnPlayMusic_Click;
            // 
            // button2
            // 
            button2.Location = new Point(0, 3);
            button2.Name = "button2";
            button2.Size = new Size(142, 94);
            button2.TabIndex = 6;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            // 
            // btnGetFiles
            // 
            btnGetFiles.Location = new Point(300, 3);
            btnGetFiles.Name = "btnGetFiles";
            btnGetFiles.Size = new Size(154, 94);
            btnGetFiles.TabIndex = 5;
            btnGetFiles.Text = "Get Files";
            btnGetFiles.UseVisualStyleBackColor = true;
            btnGetFiles.Click += btnGetFiles_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 450);
            Controls.Add(panel1);
            Controls.Add(listBoxFiles);
            Controls.Add(comboBoxClients);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ComboBox comboBoxClients;
        private ListBox listBoxFiles;
        private Panel panel1;
        private Button btnPlayMusic;
        private Button button2;
        private Button btnGetFiles;
    }
}