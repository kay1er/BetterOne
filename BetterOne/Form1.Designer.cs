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
            btnStop = new Button();
            btnDeleteFile = new Button();
            btnUploadFile = new Button();
            btnPlayMusic = new Button();
            button2 = new Button();
            btnGetFiles = new Button();
            txtServerLog = new TextBox();
            btnBrowse = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxClients
            // 
            comboBoxClients.Dock = DockStyle.Top;
            comboBoxClients.FormattingEnabled = true;
            comboBoxClients.Location = new Point(0, 0);
            comboBoxClients.Margin = new Padding(3, 4, 3, 4);
            comboBoxClients.Name = "comboBoxClients";
            comboBoxClients.Size = new Size(611, 28);
            comboBoxClients.TabIndex = 2;
            // 
            // listBoxFiles
            // 
            listBoxFiles.Dock = DockStyle.Top;
            listBoxFiles.FormattingEnabled = true;
            listBoxFiles.Location = new Point(0, 28);
            listBoxFiles.Margin = new Padding(3, 4, 3, 4);
            listBoxFiles.Name = "listBoxFiles";
            listBoxFiles.ScrollAlwaysVisible = true;
            listBoxFiles.Size = new Size(611, 164);
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
            panel1.Location = new Point(0, 467);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(611, 133);
            panel1.TabIndex = 4;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(173, 4);
            btnStop.Margin = new Padding(3, 4, 3, 4);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(82, 125);
            btnStop.TabIndex = 8;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnDeleteFile
            // 
            btnDeleteFile.Location = new Point(350, 4);
            btnDeleteFile.Margin = new Padding(3, 4, 3, 4);
            btnDeleteFile.Name = "btnDeleteFile";
            btnDeleteFile.Size = new Size(82, 125);
            btnDeleteFile.TabIndex = 9;
            btnDeleteFile.Text = "Delete File";
            btnDeleteFile.UseVisualStyleBackColor = true;
            btnDeleteFile.Click += btnDeleteFile_Click;
            // 
            // btnUploadFile
            // 
            btnUploadFile.Location = new Point(261, 4);
            btnUploadFile.Margin = new Padding(3, 4, 3, 4);
            btnUploadFile.Name = "btnUploadFile";
            btnUploadFile.Size = new Size(83, 125);
            btnUploadFile.TabIndex = 8;
            btnUploadFile.Text = "Upload File";
            btnUploadFile.UseVisualStyleBackColor = true;
            btnUploadFile.Click += btnUploadFile_Click;
            // 
            // btnPlayMusic
            // 
            btnPlayMusic.Location = new Point(83, 4);
            btnPlayMusic.Margin = new Padding(3, 4, 3, 4);
            btnPlayMusic.Name = "btnPlayMusic";
            btnPlayMusic.Size = new Size(84, 125);
            btnPlayMusic.TabIndex = 7;
            btnPlayMusic.Text = "Play";
            btnPlayMusic.UseVisualStyleBackColor = true;
            btnPlayMusic.Click += btnPlayMusic_Click;
            // 
            // button2
            // 
            button2.Location = new Point(0, 4);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(77, 125);
            button2.TabIndex = 6;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            // 
            // btnGetFiles
            // 
            btnGetFiles.Location = new Point(435, 4);
            btnGetFiles.Margin = new Padding(3, 4, 3, 4);
            btnGetFiles.Name = "btnGetFiles";
            btnGetFiles.Size = new Size(82, 125);
            btnGetFiles.TabIndex = 5;
            btnGetFiles.Text = "Get Files";
            btnGetFiles.UseVisualStyleBackColor = true;
            btnGetFiles.Click += btnGetFiles_Click;
            // 
            // txtServerLog
            // 
            txtServerLog.Location = new Point(12, 199);
            txtServerLog.Multiline = true;
            txtServerLog.Name = "txtServerLog";
            txtServerLog.ScrollBars = ScrollBars.Both;
            txtServerLog.Size = new Size(587, 261);
            txtServerLog.TabIndex = 5;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(523, 4);
            btnBrowse.Margin = new Padding(3, 4, 3, 4);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(82, 125);
            btnBrowse.TabIndex = 10;
            btnBrowse.Text = "Browse Folder";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(611, 600);
            Controls.Add(txtServerLog);
            Controls.Add(panel1);
            Controls.Add(listBoxFiles);
            Controls.Add(comboBoxClients);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
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
