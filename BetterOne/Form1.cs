using System;
using System.Collections.Generic;
using System.IO;
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

            // Gửi yêu cầu lấy danh sách file
            var message = Encoding.UTF8.GetBytes("GET_FILES");
            stream.Write(message, 0, message.Length);

            txtServerLog.Text += $"Đã gửi yêu cầu lấy danh sách file từ client {Environment.NewLine}";
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
        private void ProcessServerMessage(string message)
        {
            // Kiểm tra xem message có phải là danh sách file
            var files = message.Split('|');
            if (files.Length > 0)
            {
                Invoke(new Action(() =>
                {
                    listBoxFiles.Items.Clear();
                    foreach (var file in files)
                    {
                        if (!string.IsNullOrWhiteSpace(file))
                            listBoxFiles.Items.Add(file);
                    }
                }));

                txtServerLog.Text += $"Đã nhận và hiển thị danh sách file từ client {Environment.NewLine}";
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
            if (selectedClient == null)
            {
                txtServerLog.Text += $"Vui lòng chọn một client{Environment.NewLine}";
                return;
            }

            var client = clients[selectedClient];
            var stream = client.GetStream();

            // Gửi lệnh yêu cầu client mở Folder Dialog
            var message = Encoding.UTF8.GetBytes("BROWSE");
            stream.Write(message, 0, message.Length);

            txtServerLog.Text += "Yêu cầu client mở Folder Dialog đã được gửi.\n";
        }




    }
}
