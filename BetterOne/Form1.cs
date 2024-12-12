using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace BetterOne
{
    public partial class Form1 : Form
    {
        TcpListener server;
        Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        public Form1()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            server.BeginAcceptTcpClient(ClientConnected, null);
        }
        private void ClientConnected(IAsyncResult ar)
        {
            var client = server.EndAcceptTcpClient(ar);
            var endPoint = client.Client.RemoteEndPoint.ToString();
            clients[endPoint] = client;

            // Hiển thị client trong ComboBox
            Invoke(new Action(() =>
            {
                comboBoxClients.Items.Add(endPoint);
            }));

            // Tiếp tục chấp nhận kết nối
            server.BeginAcceptTcpClient(ClientConnected, null);
        }
        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            if (selectedClient == null || !clients.ContainsKey(selectedClient))
            {
                MessageBox.Show("Vui lòng chọn một client!");
                return;
            }

            var client = clients[selectedClient];
            var stream = client.GetStream();

            // Gửi yêu cầu lấy file
            var message = Encoding.UTF8.GetBytes("GET_FILES");
            stream.Write(message, 0, message.Length);

            // Đọc danh sách file từ client
            var buffer = new byte[4096];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            var fileList = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Hiển thị danh sách file trong ListBox
            listBoxFiles.Items.Clear();
            foreach (var file in fileList.Split('|'))
            {
                if (!string.IsNullOrWhiteSpace(file))
                    listBoxFiles.Items.Add(file);
            }
        }

        private void btnPlayMusic_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            var selectedFile = listBoxFiles.SelectedItem?.ToString();
            if (selectedClient == null || selectedFile == null)
            {
                MessageBox.Show("Vui lòng chọn một client và một file để phát!");
                return;
            }

            var client = clients[selectedClient];
            var stream = client.GetStream();

            // Gửi yêu cầu phát file
            var message = Encoding.UTF8.GetBytes($"PLAY|{selectedFile}");
            stream.Write(message, 0, message.Length);
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            if (selectedClient == null || !clients.ContainsKey(selectedClient))
            {
                MessageBox.Show("Vui lòng chọn một client!");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    var fileName = Path.GetFileName(filePath);

                    var client = clients[selectedClient];
                    var stream = client.GetStream();

                    // Notify client about incoming file
                    var message = Encoding.UTF8.GetBytes($"UPLOAD|{fileName}");
                    stream.Write(message, 0, message.Length);

                    // Send the file content
                    var fileBytes = File.ReadAllBytes(filePath);
                    stream.Write(fileBytes, 0, fileBytes.Length);

                    MessageBox.Show("File uploaded successfully!");
                }
            }
        }
    }
}
