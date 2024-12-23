namespace BetterOne
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
            btnBrowse = new Button();
            btnStop = new Button();
            btnDeleteFile = new Button();
            btnUploadFile = new Button();
            btnPlayMusic = new Button();
            button2 = new Button();
            btnGetFiles = new Button();
            txtServerLog = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxClients
            // 
            comboBoxClients.Dock = DockStyle.Top;
            comboBoxClients.FormattingEnabled = true;
            comboBoxClients.Location = new Point(0, 0);
            comboBoxClients.Name = "comboBoxClients";
            comboBoxClients.Size = new Size(535, 23);
            comboBoxClients.TabIndex = 2;
            // 
            // listBoxFiles
            // 
            listBoxFiles.Dock = DockStyle.Top;
            listBoxFiles.FormattingEnabled = true;
            listBoxFiles.ItemHeight = 15;
            listBoxFiles.Location = new Point(0, 23);
            listBoxFiles.Name = "listBoxFiles";
            listBoxFiles.ScrollAlwaysVisible = true;
            listBoxFiles.Size = new Size(535, 124);
            listBoxFiles.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnBrowse);
            panel1.Controls.Add(btnStop);
            panel1.Controls.Add(btnDeleteFile);
            panel1.Controls.Add(btnUploadFile);
            panel1.Controls.Add(btnPlayMusic);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(btnGetFiles);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 350);
            panel1.Name = "panel1";
            panel1.Size = new Size(535, 100);
            panel1.TabIndex = 4;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(458, 3);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(72, 94);
            btnBrowse.TabIndex = 10;
            btnBrowse.Text = "Browse Folder";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(151, 3);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(72, 94);
            btnStop.TabIndex = 8;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnDeleteFile
            // 
            btnDeleteFile.Location = new Point(306, 3);
            btnDeleteFile.Name = "btnDeleteFile";
            btnDeleteFile.Size = new Size(72, 94);
            btnDeleteFile.TabIndex = 9;
            btnDeleteFile.Text = "Delete File";
            btnDeleteFile.UseVisualStyleBackColor = true;
            btnDeleteFile.Click += btnDeleteFile_Click;
            // 
            // btnUploadFile
            // 
            btnUploadFile.Location = new Point(228, 3);
            btnUploadFile.Name = "btnUploadFile";
            btnUploadFile.Size = new Size(73, 94);
            btnUploadFile.TabIndex = 8;
            btnUploadFile.Text = "Upload File";
            btnUploadFile.UseVisualStyleBackColor = true;
            btnUploadFile.Click += btnUploadFile_Click;
            // 
            // btnPlayMusic
            // 
            btnPlayMusic.Location = new Point(73, 3);
            btnPlayMusic.Name = "btnPlayMusic";
            btnPlayMusic.Size = new Size(74, 94);
            btnPlayMusic.TabIndex = 7;
            btnPlayMusic.Text = "Play";
            btnPlayMusic.UseVisualStyleBackColor = true;
            btnPlayMusic.Click += btnPlayMusic_Click;
            // 
            // button2
            // 
            button2.Location = new Point(0, 3);
            button2.Name = "button2";
            button2.Size = new Size(67, 94);
            button2.TabIndex = 6;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            // 
            // btnGetFiles
            // 
            btnGetFiles.Location = new Point(381, 3);
            btnGetFiles.Name = "btnGetFiles";
            btnGetFiles.Size = new Size(72, 94);
            btnGetFiles.TabIndex = 5;
            btnGetFiles.Text = "Get Files";
            btnGetFiles.UseVisualStyleBackColor = true;
            btnGetFiles.Click += btnGetFiles_Click;
            // 
            // txtServerLog
            // 
            txtServerLog.Location = new Point(0, 148);
            txtServerLog.Margin = new Padding(3, 2, 3, 2);
            txtServerLog.Multiline = true;
            txtServerLog.Name = "txtServerLog";
            txtServerLog.ScrollBars = ScrollBars.Both;
            txtServerLog.Size = new Size(535, 197);
            txtServerLog.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(535, 450);
            Controls.Add(txtServerLog);
            Controls.Add(panel1);
            Controls.Add(listBoxFiles);
            Controls.Add(comboBoxClients);
            Name = "Form1";
            Text = "Server";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBoxClients;
        private ListBox listBoxFiles;
        private Panel panel1;
        private Button btnPlayMusic;
        private Button button2;
        private Button btnGetFiles;
        private Button btnUploadFile;
        private Button btnDeleteFile;
        private Button btnStop;
        private TextBox txtServerLog;
        private Button btnBrowse;
    }
}
