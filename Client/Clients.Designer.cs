namespace Client
{
    partial class Clients
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
            btnConnect = new Button();
            btnBrowseFolder = new Button();
            listBoxLog = new ListBox();
            txtServerAddress = new TextBox();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 313);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(167, 125);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnBrowseFolder
            // 
            btnBrowseFolder.Location = new Point(225, 313);
            btnBrowseFolder.Name = "btnBrowseFolder";
            btnBrowseFolder.Size = new Size(167, 125);
            btnBrowseFolder.TabIndex = 1;
            btnBrowseFolder.Text = "Browse";
            btnBrowseFolder.UseVisualStyleBackColor = true;
            btnBrowseFolder.Click += btnBrowseFolder_Click;
            // 
            // listBoxLog
            // 
            listBoxLog.FormattingEnabled = true;
            listBoxLog.ItemHeight = 15;
            listBoxLog.Location = new Point(12, 50);
            listBoxLog.Name = "listBoxLog";
            listBoxLog.ScrollAlwaysVisible = true;
            listBoxLog.Size = new Size(380, 244);
            listBoxLog.TabIndex = 2;
            // 
            // txtServerAddress
            // 
            txtServerAddress.Location = new Point(12, 12);
            txtServerAddress.Name = "txtServerAddress";
            txtServerAddress.Size = new Size(380, 23);
            txtServerAddress.TabIndex = 3;
            // 
            // Clients
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 450);
            Controls.Add(txtServerAddress);
            Controls.Add(listBoxLog);
            Controls.Add(btnBrowseFolder);
            Controls.Add(btnConnect);
            Name = "Clients";
            Text = "Clients";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Button btnBrowseFolder;
        private ListBox listBoxLog;
        private TextBox txtServerAddress;
    }
}