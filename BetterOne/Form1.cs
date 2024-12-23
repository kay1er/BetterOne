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
                txtServerLog.Text += $"Client {endPoint} đã kết nối thành công {Environment.NewLine}";
            }));

            // Tiếp tục chấp nhận kết nối
            server.BeginAcceptTcpClient(ClientConnected, null);
        }
        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            if (selectedClient == null || !clients.ContainsKey(selectedClient))
            {
                txtServerLog.Text += $"Vui lòng chọn một client {Environment.NewLine}";
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
            txtServerLog.Text += $"Đã gửi yêu cầu hiển thị file {Environment.NewLine}";
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            var selectedFile = listBoxFiles.SelectedItem?.ToString();
            if (selectedFile == null && selectedClient == null)
            {
                MessageBox.Show("Error");
                return;
            }
            var client = clients[selectedClient];
            var stream = client.GetStream();
            var message = Encoding.UTF8.GetBytes($"STOP|{selectedFile}");
            txtServerLog.Text += $"Đã gửi yêu cầu dừng nhạc {Environment.NewLine}";
            stream.Write(message, 0, message.Length);
        }
        private void btnPlayMusic_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            var selectedFile = listBoxFiles.SelectedItem?.ToString();
            if (selectedClient == null || selectedFile == null)
            {
                txtServerLog.Text += $"Vui lòng chọn một client và một file để phát nhạc";
                return;
            }

            var client = clients[selectedClient];
            var stream = client.GetStream();

            // Gửi yêu cầu phát file
            var message = Encoding.UTF8.GetBytes($"PLAY|{selectedFile}");
            txtServerLog.Text += $"Đã gửi yêu cầu phát nhạc {Environment.NewLine}";
            stream.Write(message, 0, message.Length);
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            if (selectedClient == null || !clients.ContainsKey(selectedClient))
            {
                txtServerLog.Text += $"Vui lòng chọn một client {Environment.NewLine}";
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    var fileName = Path.GetFileName(filePath);
                    var fileBytes = File.ReadAllBytes(filePath);
                    var fileSize = fileBytes.Length;

                    var client = clients[selectedClient];
                    var stream = client.GetStream();

                    // Gửi lệnh upload và tên file
                    var message = Encoding.UTF8.GetBytes($"UPLOAD|{fileName}|{fileSize}");
                    stream.Write(message, 0, message.Length);

                    // Gửi nội dung file
                    stream.Write(fileBytes, 0, fileSize);

                    txtServerLog.Text += $"Gửi file thành công {Environment.NewLine}";
                }
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            var selectedFile = listBoxFiles.SelectedItem?.ToString();
            if (selectedClient == null || selectedFile == null)
            {
                txtServerLog.Text += $"Vui lòng chọn một client và một file để xóa {Environment.NewLine}";
                return;
            }

            var client = clients[selectedClient];
            var stream = client.GetStream();

            // Gửi lệnh xóa file
            var message = Encoding.UTF8.GetBytes($"DELETE|{selectedFile}");
            stream.Write(message, 0, message.Length);

            txtServerLog.Text += $"Yêu cầu xóa file đã được gửi!";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString();
            var selectedFile = listBoxFiles.SelectedItems?.ToString();
            if (selectedClient == null || selectedFile == null)
            {
                txtServerLog.Text += $"Vui lòng chọn client{Environment.NewLine}";
            }
            var client = clients[selectedClient];
            var stream = client.GetStream();
            var message = Encoding.UTF8.GetBytes($"BROWSE|");
            txtServerLog.Text += $"Yêu cầu chọn thư mục đã được gửi {Environment.NewLine}";
        }
    }
}