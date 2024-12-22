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
            listBoxLog = new ListBox();
            txtServerAddress = new TextBox();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(14, 417);
            btnConnect.Margin = new Padding(3, 4, 3, 4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(420, 167);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // listBoxLog
            // 
            listBoxLog.FormattingEnabled = true;
            listBoxLog.Location = new Point(14, 67);
            listBoxLog.Margin = new Padding(3, 4, 3, 4);
            listBoxLog.Name = "listBoxLog";
            listBoxLog.ScrollAlwaysVisible = true;
            listBoxLog.Size = new Size(434, 324);
            listBoxLog.TabIndex = 2;
            // 
            // txtServerAddress
            // 
            txtServerAddress.Location = new Point(14, 16);
            txtServerAddress.Margin = new Padding(3, 4, 3, 4);
            txtServerAddress.Name = "txtServerAddress";
            txtServerAddress.Size = new Size(434, 27);
            txtServerAddress.TabIndex = 3;
            // 
            // Clients
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 600);
            Controls.Add(txtServerAddress);
            Controls.Add(listBoxLog);
            Controls.Add(btnConnect);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Clients";
            Text = "Clients";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private ListBox listBoxLog;
        private TextBox txtServerAddress;
    }
}